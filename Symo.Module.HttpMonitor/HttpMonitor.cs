using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using Symo.Module.HttpMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symo.Module.HttpMonitor
{
    [MonitorControls("HTTP-based monitoring", typeof(HttpMonitorControl), typeof(HttpMonitorConfigControl))]
    public class HttpMonitor : IMonitor
    {
        private IServer _server;
        private IConfiguration _configuration;
        private ConnectionState _state;

        private uint _failCount;

        private Task _worker;
        private CancellationTokenSource _cancellationTokenSource;

        public IConfiguration Configuration { get { return _configuration; } set { _configuration = value; } }
        public IServer Server { get { return _server; } set { _server = value; } }

        public bool IsRunning
        {
            get
            {
                return _worker.Status == TaskStatus.Running ||
                    _worker.Status == TaskStatus.WaitingToRun;
            }
        }
        public ConnectionState Status { get { return _state; } }
        public uint TotalRequestFailure { get; set; }
        public uint TotalRequestsSent { get; set; }
        public decimal RequestFailureRate { get { return (decimal)TotalRequestFailure / TotalRequestsSent; } }

        public event UpdateStateEventHandler StateUpdated;
        public event TriggerEventHandler Triggered;

        public HttpMonitor()
        {
            _failCount = 0;

            _cancellationTokenSource = new CancellationTokenSource();
            _worker = new Task(() =>
            {
                try
                {
                    while (true)
                    {
                        try
                        {
                            var http = WebRequest.CreateHttp(_server.Address);
                            var reply = (HttpWebResponse)http.GetResponse();
                            if (reply.StatusCode == HttpStatusCode.OK)
                            {
                                if (_state != ConnectionState.HEALTHY)
                                {
                                    _state = ConnectionState.HEALTHY;
                                    StateUpdated?.Invoke(this, _state);
                                    _failCount = 0;
                                }
                            }
                            else
                            {
                                HandleFailure();
                            }
                        } catch (WebException)
                        {
                            HandleFailure();
                        }
                        TotalRequestsSent++;

                        Triggered?.Invoke(this);
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(_configuration.ChallengeDelay);
                    }

                }
                catch (OperationCanceledException ex)
                {
                    if (ex.CancellationToken.IsCancellationRequested)
                    {
                        StateUpdated(this, ConnectionState.UNKNOWN);
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        private void HandleFailure()
        {
            _failCount++;
            TotalRequestFailure++;

            if (_failCount == _configuration.FailThreshold - 1 && _state != ConnectionState.FAILED)
            {
                _state = ConnectionState.FAILED;
                StateUpdated?.Invoke(this, _state);
            }

            if (_state != ConnectionState.FAILED)
            {
                _state = ConnectionState.UNHEALTY;
                StateUpdated?.Invoke(this, _state);
            }
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _worker.Start();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                _cancellationTokenSource.Cancel();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}

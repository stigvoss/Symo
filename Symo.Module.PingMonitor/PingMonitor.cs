using Symo.Library.Extensibility;
using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Common;
using Symo.Library.Extensibility.Interfaces;
using Symo.Module.PingMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symo.Module.PingMonitor
{
    [MonitorControls("Ping-based monitoring", typeof(PingMonitorControl), typeof(PingMonitorConfigControl))]
    public class PingMonitor : IMonitor
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
        public uint TotalPackageLoss { get; set; }
        public uint TotalPackageSent { get; set; }
        public decimal PackageLossRate { get { return (decimal)TotalPackageLoss / TotalPackageSent; } }

        public event UpdateStateEventHandler StateUpdated;
        public event TriggerEventHandler Triggered;

        public PingMonitor()
        {
            _failCount = 0;

            _cancellationTokenSource = new CancellationTokenSource();
            _worker = new Task(() =>
            {
                try
                {
                    var ping = new Ping();

                    while (true)
                    {
                        var reply = ping.Send(_server.Address);
                        if (reply.Status == IPStatus.Success)
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
                            _failCount++;
                            TotalPackageLoss++;

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
                        TotalPackageSent++;

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

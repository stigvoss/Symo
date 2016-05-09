using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Symo.Library.Extensibility.Common
{
    public abstract class Monitor : IMonitor
    {
        private IServer _server;
        private IConfiguration _configuration;

        protected Task _workerThread;
        protected CancellationTokenSource _cancellationTokenSource;

        public event UpdateStateEventHandler StateUpdated;
        public event TriggerEventHandler Triggered;

        public virtual bool IsRunning
        {
            get
            {
                return _workerThread != null && (
                    _workerThread.Status == TaskStatus.Running ||
                    _workerThread.Status == TaskStatus.WaitingToRun
                    );
            }
        }
        public IConfiguration Configuration { get { return _configuration; } set { _configuration = value; } }
        public IServer Server { get { return _server; } set { _server = value; } }
        
        private Task GetTask()
        {
            return new Task(() =>
            {
                try
                {
                    while (true)
                    {
                        Monitoring();

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
            });
        }

        protected abstract void Monitoring();

        protected virtual void OnTriggered()
        {
            Triggered?.Invoke(this);
        }

        protected virtual void OnStateUpdated(ConnectionState state)
        {
            StateUpdated?.Invoke(this, state);
        }

        public virtual void Start()
        {
            if(_workerThread == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _workerThread = GetTask();
            }

            if (!IsRunning)
            {
                _workerThread.Start();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public virtual void Stop()
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

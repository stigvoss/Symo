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
    [ModuleInfo("HTTP-based monitoring", typeof(HttpMonitorControl), typeof(HttpMonitorConfigControl))]
    public class HttpMonitor : Library.Extensibility.Common.Monitor
    {
        private IEnumerable<HttpStatusCode> _acceptableStatusCodes = new List<HttpStatusCode>
        {
            HttpStatusCode.OK,
            HttpStatusCode.Redirect,
            HttpStatusCode.Accepted,
            HttpStatusCode.Continue,
            HttpStatusCode.Moved,
            HttpStatusCode.MovedPermanently
        };
        private ConnectionState _state;

        private uint _failCount;

        public ConnectionState Status { get { return _state; } }
        public uint TotalRequestFailure { get; set; }
        public uint TotalRequestsSent { get; set; }
        public decimal RequestFailureRate { get { return (decimal)TotalRequestFailure / TotalRequestsSent; } }

        public HttpMonitor()
        {
            _failCount = 0;
        }

        private void HandleFailure()
        {
            _failCount++;
            TotalRequestFailure++;

            if (_failCount == Configuration.FailThreshold - 1 && _state != ConnectionState.FAILED)
            {
                _state = ConnectionState.FAILED;
                OnStateUpdated(_state);
            }

            if (_state != ConnectionState.FAILED)
            {
                _state = ConnectionState.UNHEALTY;
                OnStateUpdated(_state);
            }
        }

        protected override void Monitoring()
        {
            try
            {
                var http = WebRequest.CreateHttp(Server.Address);
                var reply = (HttpWebResponse)http.GetResponse();
                if (_acceptableStatusCodes.Contains(reply.StatusCode))
                {
                    if (_state != ConnectionState.HEALTHY)
                    {
                        _state = ConnectionState.HEALTHY;
                        OnStateUpdated(_state);
                        _failCount = 0;
                    }
                }
                else
                {
                    HandleFailure();
                }
            }
            catch (WebException)
            {
                HandleFailure();
            }
            TotalRequestsSent++;

            OnTriggered();
        }
    }
}

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
    [ModuleInfo("Ping-based monitoring", typeof(PingMonitorControl), typeof(PingMonitorConfigControl))]
    public class PingMonitor : Library.Extensibility.Common.Monitor
    {
        private ConnectionState _state;

        private uint _failCount;

        public ConnectionState Status { get { return _state; } }
        public uint TotalPackageLoss { get; set; }
        public uint TotalPackageSent { get; set; }
        public decimal PackageLossRate { get { return (decimal)TotalPackageLoss / TotalPackageSent; } }

        public PingMonitor()
        {
            _failCount = 0;
        }

        protected override void Monitoring()
        {
            var ping = new Ping();

            var reply = ping.Send(Server.Address);
            if (reply.Status == IPStatus.Success)
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
                _failCount++;
                TotalPackageLoss++;

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
            TotalPackageSent++;

            OnTriggered();
        }
    }
}

using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Module.PingMonitor
{
    public class PingMonitorConfiguration : IConfiguration
    {
        public int ChallengeDelay { get; set; }

        public int FailThreshold { get; set; }
    }
}

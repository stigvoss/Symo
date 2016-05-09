using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Module.PingMonitor
{
    public class PingInfo
    {
        public DateTime Time { get; set; }
        public int ResponseTime { get; set; }
        public IPStatus Reply { get; set; }
    }
}

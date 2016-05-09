using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Monitoring
{
    public class MonitorInfo
    {
        public IMonitor Monitor { get; internal set; }
    }
}

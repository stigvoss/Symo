using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Entities
{
    public class ConfiguredEventArgs : EventArgs
    {
        public IServer Server { get; set; }
        public IConfiguration Configuration { get; set; }
        public Type MonitorType { get; set; }
    }
}

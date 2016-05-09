using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Persistence
{
    public class MonitorPersistence
    {
        public void Store(IMonitor monitor)
        {
            var monitorType = monitor.GetType();

            var properties = monitorType.GetProperties();

            foreach(var property in properties)
            {
                var value = property.GetValue(monitor);
            }
        }
    }
}

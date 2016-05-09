using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Common
{
    public static class UserControlFactory
    {
        public static IMonitorUserControl GetMonitorControl(MonitorControlsAttribute attribute)
        {
            return (IMonitorUserControl)Activator.CreateInstance(attribute.MonitorControl);
        }

        public static IConfigUserControl GetConfigControl(MonitorControlsAttribute attribute)
        {
            return (IConfigUserControl)Activator.CreateInstance(attribute.ConfigControl);
        }
    }
}

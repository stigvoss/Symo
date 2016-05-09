using Symo.Library.Extensibility.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Common
{
    public static class AttributeLoader
    {
        public static MonitorControlsAttribute GetMonitorControlsAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(MonitorControlsAttribute), true);
            var controlsAttributes = (MonitorControlsAttribute[])attributes;
            return controlsAttributes.FirstOrDefault();
        }
    }
}

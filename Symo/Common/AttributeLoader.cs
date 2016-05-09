using Symo.Library.Extensibility.Attributes;
using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Common
{
    public static class AttributeLoader
    {
        public static ModuleInfoAttribute GetMonitorControlsAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(ModuleInfoAttribute), true);
            var controlsAttributes = (ModuleInfoAttribute[])attributes;
            return controlsAttributes.FirstOrDefault();
        }
    }
}

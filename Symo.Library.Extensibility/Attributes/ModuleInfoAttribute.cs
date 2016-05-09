using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Extensibility.Attributes
{
    public class ModuleInfoAttribute : Attribute
    {
        public string Name { get; set; }
        public Type MonitorControl { get; set; }
        public Type ConfigControl { get; set; }

        public ModuleInfoAttribute(string name, Type displayControl, Type configControl)
        {
            Name = name;

            if(displayControl.GetInterfaces().Contains(typeof(IMonitorUserControl)))
            {
                MonitorControl = displayControl;
            }
            
            if (configControl.GetInterfaces().Contains(typeof(IConfigUserControl)))
            {
                ConfigControl = configControl;
            }
        }
    }
}

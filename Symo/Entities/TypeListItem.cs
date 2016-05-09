using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Entities
{
    public class TypeListItem
    {
        public IConfigUserControl ConfigControl { get; internal set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}

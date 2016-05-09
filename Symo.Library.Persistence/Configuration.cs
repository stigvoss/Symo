using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Persistence
{
    public class Configuration
    {
        public int ID { get; set; }

        public virtual List<KeyValue> KeyValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Extensibility.Interfaces
{
    public interface IConfigUserControl
    {
        IServer Server { get; }
        IConfiguration Configuration { get; }
    }
}

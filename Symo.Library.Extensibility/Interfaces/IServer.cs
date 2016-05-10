using Symo.Library.Extensibility.Attributes;

namespace Symo.Library.Extensibility.Interfaces
{
    public interface IServer
    {
        [Persist]
        string Name { get; set; }

        [Persist]
        string Address { get; set; }
    }
}
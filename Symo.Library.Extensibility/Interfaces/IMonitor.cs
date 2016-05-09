using Symo.Library.Extensibility.Common;

namespace Symo.Library.Extensibility.Interfaces
{
    public delegate void UpdateStateEventHandler(object sender, ConnectionState state);

    public delegate void TriggerEventHandler(object sender);

    public interface IMonitor
    {
        IServer Server { get; set; }

        IConfiguration Configuration { get; set; }

        event UpdateStateEventHandler StateUpdated;

        event TriggerEventHandler Triggered;

        bool IsRunning { get; }

        void Start();

        void Stop();
    }
}
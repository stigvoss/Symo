using Symo.Library.Extensibility.Attributes;

namespace Symo.Library.Extensibility.Interfaces
{
    public interface IConfiguration
    {
        [Persist]
        int FailThreshold { get; }

        [Persist]
        int ChallengeDelay { get; }
    }
}
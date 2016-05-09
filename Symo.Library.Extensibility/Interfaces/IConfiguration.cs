namespace Symo.Library.Extensibility.Interfaces
{
    public interface IConfiguration
    {
        int FailThreshold { get; }
        int ChallengeDelay { get; }
    }
}
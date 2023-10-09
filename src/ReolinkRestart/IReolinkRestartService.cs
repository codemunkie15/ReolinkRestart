namespace ReolinkRestart
{
    public interface IReolinkRestartService
    {
        DateTime? NextRestartAtUtc { get; }

        void Start();
    }
}

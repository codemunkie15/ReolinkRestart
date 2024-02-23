namespace ReolinkRestart.ReolinkClient
{
    public interface IReolinkClientService
    {
        void Start();

        void Terminate(bool waitForExit = false);

        bool IsClientRunning();

        void SetFullScreen();
    }
}

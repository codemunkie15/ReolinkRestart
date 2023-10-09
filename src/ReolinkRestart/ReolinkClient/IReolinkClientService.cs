namespace ReolinkRestart.ReolinkClient
{
    public interface IReolinkClientService
    {
        void Start();

        void Terminate();

        bool IsClientRunning();

        void SetFullScreen();
    }
}

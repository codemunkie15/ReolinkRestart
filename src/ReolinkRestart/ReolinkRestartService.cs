using System.Timers;
using ReolinkRestart.ReolinkClient;
using ReolinkRestart.Settings;
using Timer = System.Timers.Timer;

namespace ReolinkRestart
{
    public class ReolinkRestartService : IReolinkRestartService
    {
        public DateTime? NextRestartAtUtc { get; private set; }

        private ISettingsService settingsService;
        private IReolinkClientService reolinkClientService;
        private Timer restartTimer;

        public ReolinkRestartService(IReolinkClientService reolinkClientService, ISettingsService settingsService)
        {
            this.reolinkClientService = reolinkClientService;
            this.settingsService = settingsService;
            this.restartTimer = new Timer();
        }

        public void Start()
        {
            restartTimer.Interval = settingsService.Settings!.RestartIntervalMinutes * 60 * 1000;
            restartTimer.Elapsed += RestartTimer_Elapsed;
            restartTimer.Start();
            SetNextRestartAtUtc();

            if (!reolinkClientService.IsClientRunning())
            {
                reolinkClientService.Start();
            }
        }

        private void RestartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SetNextRestartAtUtc();
            reolinkClientService.Terminate(true);
            reolinkClientService.Start();
        }

        private void SetNextRestartAtUtc()
        {
            NextRestartAtUtc = DateTime.UtcNow.AddMinutes(settingsService.Settings.RestartIntervalMinutes);
        }
    }
}

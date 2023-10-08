using System.Timers;
using Timer = System.Timers.Timer;

namespace ReolinkRestart
{
    public class ReolinkRestartService
    {
        private const int RestartIntervalMinutes = 1;

        public DateTime? NextRestartAtUtc { get; private set; }

        private ReolinkClient reolinkClient = new();
        private Timer restartTimer = new Timer();

        public void Start()
        {
            if (!reolinkClient.IsClientRunning())
            {
                reolinkClient.Start();
            }

            restartTimer.Interval = RestartIntervalMinutes * 60 * 1000;
            restartTimer.Elapsed += RestartTimer_Elapsed;
            restartTimer.Start();
            SetNextRestartAtUtc();
        }

        private void RestartTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            SetNextRestartAtUtc();
            reolinkClient.Terminate();
            reolinkClient.Start();
        }

        private void SetNextRestartAtUtc()
        {
            NextRestartAtUtc = DateTime.UtcNow.AddMinutes(RestartIntervalMinutes);
        }
    }
}

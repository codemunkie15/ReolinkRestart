using ReolinkRestart.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ReolinkRestart
{
    public class ReolinkRestartApplicationContext : ApplicationContext
    {
        private readonly IReolinkRestartService reolinkRestartService;
        private NotifyIcon trayIcon;
        private Timer menuUpdateTimer = new Timer();
        private ToolStripLabel nextRestartMenuLabel;

        public ReolinkRestartApplicationContext(IReolinkRestartService reolinkRestartService)
        {
            this.reolinkRestartService = reolinkRestartService;

            Application.ApplicationExit += Application_ApplicationExit;

            trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                Visible = true,
                ContextMenuStrip = BuildMenu()
            };

            menuUpdateTimer.Interval = 1000;
            menuUpdateTimer.Tick += MenuUpdateTimer_Tick;
            menuUpdateTimer.Start();

            Task.Run(() => reolinkRestartService.Start());
        }

        private ContextMenuStrip BuildMenu()
        {
            nextRestartMenuLabel = new ToolStripLabel($"Next restart:");

            return new ContextMenuStrip()
            {
                Items =
                    {
                        nextRestartMenuLabel,
                        new ToolStripSeparator(),
                        new ToolStripMenuItem("Exit", null, Exit)
                    }
            };
        }

        private void MenuUpdateTimer_Tick(object? sender, EventArgs e)
        {
            nextRestartMenuLabel.Text = $"Next restart: {GetNextRestart():hh':'mm':'ss}";
        }

        private TimeSpan GetNextRestart()
        {
            if (!reolinkRestartService.NextRestartAtUtc.HasValue)
            {
                return TimeSpan.Zero;
            }

            return reolinkRestartService.NextRestartAtUtc.Value - DateTime.UtcNow;
        }

        private void Exit(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Application_ApplicationExit(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
        }
    }
}
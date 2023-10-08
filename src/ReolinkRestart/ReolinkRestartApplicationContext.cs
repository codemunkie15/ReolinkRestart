using ReolinkRestart.Properties;
using Timer = System.Windows.Forms.Timer;

namespace ReolinkRestart
{
    public class ReolinkRestartApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ReolinkRestartService service = new ReolinkRestartService();
        private Timer menuUpdateTimer = new Timer();
        private ToolStripLabel nextRestartMenuLabel;

        public ReolinkRestartApplicationContext()
        {
            nextRestartMenuLabel = new ToolStripLabel($"Next restart:");

            trayIcon = new NotifyIcon
            {
                Icon = Resources.AppIcon,
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip()
                {
                    Items =
                    {
                        nextRestartMenuLabel,
                        new ToolStripSeparator(),
                        new ToolStripMenuItem("Exit", null, Exit)
                    }
                }
            };

            menuUpdateTimer.Interval = 1000;
            menuUpdateTimer.Tick += MenuUpdateTimer_Tick;
            menuUpdateTimer.Start();

            Task.Run(() => service.Start());
        }

        private void MenuUpdateTimer_Tick(object? sender, EventArgs e)
        {
            nextRestartMenuLabel.Text = $"Next restart: {GetNextRestart():hh':'mm':'ss}";
        }

        private TimeSpan GetNextRestart()
        {
            if (!service.NextRestartAtUtc.HasValue)
            {
                return TimeSpan.Zero;
            }

            return service.NextRestartAtUtc.Value - DateTime.UtcNow;
        }

        private void Exit(object? sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
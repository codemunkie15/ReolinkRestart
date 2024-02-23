using System.Diagnostics;
using System.Runtime.InteropServices;
using ReolinkRestart.Settings;
using Vanara.PInvoke;

namespace ReolinkRestart.ReolinkClient
{
    public class ReolinkClientService : IReolinkClientService
    {
        private const string ReolinkProcessName = "Reolink";

        private readonly ISettingsService settingsService;

        public ReolinkClientService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public void Start()
        {
            Process.Start(settingsService.Settings!.ReolinkExecutableFilePath);
            if (settingsService.Settings!.UseFullscreen)
            {
                Thread.Sleep(settingsService.Settings!.FullscreenDelaySeconds * 1000);
                SetFullScreen();
            }
        }

        public void Terminate()
        {
            var process = GetClientProcess();
            process?.CloseMainWindow();
        }

        public bool IsClientRunning()
        {
            return GetClientProcess() is not null;
        }

        public void SetFullScreen()
        {
            var process = GetClientProcess();

            if (process != null)
            {
                User32.GetWindowRect(process.MainWindowHandle, out var windowRect);
                Cursor.Position = new Point(windowRect.Right - settingsService.Settings!.FullscreenXOffset, windowRect.Bottom - settingsService.Settings!.FullscreenYOffset);
                User32.SendInput(1, new[] {
                    new User32.INPUT {
                        mi = new User32.MOUSEINPUT
                        {
                            dwFlags = User32.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF.MOUSEEVENTF_LEFTUP
                        }
                    }
                }, Marshal.SizeOf(new User32.INPUT()));
            }
        }

        private static Process? GetClientProcess()
        {
            return Process.GetProcessesByName(ReolinkProcessName).FirstOrDefault();
        }
    }
}

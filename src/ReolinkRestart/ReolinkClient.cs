using System.Diagnostics;
using System.Runtime.InteropServices;
using Vanara.PInvoke;

namespace ReolinkRestart
{
    public class ReolinkClient
    {
        private const string ReolinkProcessName = "Reolink";
        private const string ReolinkExeLocation = @"C:\Users\callu\AppData\Local\Programs\Reolink\Reolink.exe";

        public void Start()
        {
            Process.Start(ReolinkExeLocation);
            Thread.Sleep(3000);
            SetFullScreen();
        }

        public void Terminate()
        {
            var process = GetClientProcess();
            if (process is not null)
            {
                process.Kill();
            }
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
                Cursor.Position = new Point(windowRect.Right - 50, windowRect.Bottom - 40);
                //User32.mouse_event(User32.MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN | User32.MOUSEEVENTF.MOUSEEVENTF_LEFTUP, )
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

        private Process? GetClientProcess()
        {
            return Process.GetProcessesByName(ReolinkProcessName).FirstOrDefault();
        }
    }
}

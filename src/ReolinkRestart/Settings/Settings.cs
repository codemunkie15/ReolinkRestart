namespace ReolinkRestart.Settings
{
    public class Settings
    {
        public string ReolinkExecutableFilePath { get; set; } = @"%localAppData%\Programs\Reolink\Reolink.exe";

        public int RestartIntervalMinutes { get; set; } = 60;

        public bool UseFullscreen { get; set; } = true;

        public int FullscreenDelaySeconds { get; set; } = 4;

        public int FullscreenXOffset { get; set; } = 50;

        public int FullscreenYOffset { get; set; } = 40;

        public bool ReolinkClientGracefulExit { get; set; } = true;
    }
}

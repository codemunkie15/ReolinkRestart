namespace ReolinkRestart.Settings
{
    public class Settings
    {
        public required string ReolinkExecutableFilePath { get; set; }

        public required int RestartIntervalMinutes { get; set; }

        public required bool UseFullscreen { get; set; }

        public required int FullscreenDelaySeconds { get; set; }

        public required int FullscreenXOffset { get; set; }

        public required int FullscreenYOffset { get; set; }
    }
}

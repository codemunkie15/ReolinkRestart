using System.Text.Json;
using System.Windows.Forms;

namespace ReolinkRestart.Settings
{
    public class SettingsService : ISettingsService
    {
        private const string SettingsFileName = "settings.json";

        public Settings? Settings { get; private set; }

        public bool ValidateAndLoadSettings(out string? message)
        {
            if (!SettingsExist())
            {
                CreateSettings();
            }

            if (ValidateAndLoadSettingsFromFile(out var validationMessage))
            {
                message = null;
                return true;
            }

            message = validationMessage;
            return false;
        }

        private void CreateSettings()
        {
            var settings = new Settings
            {
                ReolinkExecutableFilePath = @"%localAppData%\Programs\Reolink\Reolink.exe",
                RestartIntervalMinutes = 60,
                UseFullscreen = true,
                FullscreenDelaySeconds = 4,
                FullscreenXOffset = 50,
                FullscreenYOffset = 40
            };

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFileName, json);
        }

        private void LoadSettings()
        {
            var json = File.ReadAllText(SettingsFileName);
            Settings = JsonSerializer.Deserialize<Settings>(json)!;

            Settings!.ReolinkExecutableFilePath = Environment.ExpandEnvironmentVariables(Settings.ReolinkExecutableFilePath);
        }

        private bool ValidateAndLoadSettingsFromFile(out string validationMessage)
        {
            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                validationMessage = ex.Message;
                return false;
            }

            if (!File.Exists(Settings!.ReolinkExecutableFilePath))
            {
                validationMessage = $"{Settings.ReolinkExecutableFilePath} does not exist.";
                return false;
            }

            validationMessage = "";
            return true;
        }

        private bool SettingsExist()
        {
            return File.Exists(SettingsFileName);
        }
    }
}

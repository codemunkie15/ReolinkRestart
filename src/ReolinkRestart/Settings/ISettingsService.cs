namespace ReolinkRestart.Settings
{
    public interface ISettingsService
    {
        Settings? Settings { get; }

        bool ValidateAndLoadSettings(out string? message);
    }
}

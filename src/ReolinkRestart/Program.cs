using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReolinkRestart.ReolinkClient;
using ReolinkRestart.Settings;

namespace ReolinkRestart
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            var host = CreateHostBuilder().Build();
            var serviceProvider = host.Services;

            var settingsService = serviceProvider.GetRequiredService<ISettingsService>();

            if (settingsService.ValidateAndLoadSettings(out var validationMessage))
            {
                Application.Run(serviceProvider.GetRequiredService<ReolinkRestartApplicationContext>());
            }
            else
            {
                MessageBox.Show($"Settings.json is invalid.{Environment.NewLine}{Environment.NewLine}{validationMessage}", "Error");
            }
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ReolinkRestartApplicationContext>();

                    services.AddSingleton<IReolinkClientService, ReolinkClientService>();
                    services.AddSingleton<IReolinkRestartService, ReolinkRestartService>();
                    services.AddSingleton<ISettingsService, SettingsService>();
                });
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message, "Error");
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error");
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error");
        }
    }
}
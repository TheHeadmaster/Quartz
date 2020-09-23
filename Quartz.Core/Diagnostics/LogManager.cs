using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Librarium.Core;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace Quartz.Core.Diagnostics
{
    /// <summary>
    /// Manages logging setup and functions.
    /// </summary>
    public static class LogManager
    {
        private static string logPath = "";

        /// <summary>
        /// Sets up the color theme for ESDashboard.
        /// </summary>
        private static AnsiConsoleTheme QuartzTheme { get; } = new AnsiConsoleTheme(
            new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = "\x1b[38;5;0229m",
                [ConsoleThemeStyle.SecondaryText] = "\x1b[38;5;0246m",
                [ConsoleThemeStyle.TertiaryText] = "\x1b[38;5;0242m",
                [ConsoleThemeStyle.Invalid] = "\x1b[33;1m",
                [ConsoleThemeStyle.Null] = "\x1b[38;5;0038m",
                [ConsoleThemeStyle.Name] = "\x1b[38;5;0081m",
                [ConsoleThemeStyle.String] = "\x1b[38;5;0216m",
                [ConsoleThemeStyle.Number] = "\x1b[38;5;151m",
                [ConsoleThemeStyle.Boolean] = "\x1b[38;5;0038m",
                [ConsoleThemeStyle.Scalar] = "\x1b[38;5;0079m",
                [ConsoleThemeStyle.LevelVerbose] = "\x1b[197m",
                [ConsoleThemeStyle.LevelDebug] = "\x1b[089m",
                [ConsoleThemeStyle.LevelInformation] = "\x1b[37;163m",
                [ConsoleThemeStyle.LevelWarning] = "\x1b[38;5;0226m",
                [ConsoleThemeStyle.LevelError] = "\x1b[38;5;0160m",
                [ConsoleThemeStyle.LevelFatal] = "\x1b[38;5;0124m",
            });

        /// <summary>
        /// Initializes the log manager. Logging sinks are set up here.
        /// </summary>
        public static void Initialize()
        {
            if (logPath.IsNullOrWhiteSpace())
            {
                DateTime dt = DateTime.UtcNow;
                string filename = $"{dt.Month}-{dt.Day}-{dt.Year} {dt.Hour}-{dt.Minute}-{dt.Second}";
                logPath = Path.Combine(AppMeta.AppDataDirectory, "Quartz", "Logs", $"{filename}.json");
            }
            if (!Directory.Exists(Path.Combine(AppMeta.AppDataDirectory, "Quartz", "Logs")))
            {
                Directory.CreateDirectory(Path.Combine(AppMeta.AppDataDirectory, "Quartz", "Logs"));
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}", theme: QuartzTheme)
                .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Seq("http://localhost:5341")
                .WriteTo.File(new CompactJsonFormatter(), logPath)
                .CreateLogger();

            Log.Information("Logging to the output tab.");
            Log.Information("Logging to Seq. Open http://localhost:5341 for more details.");
            Log.Information("Logging to compact json file. Check the 'Logs' folder.");
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Librarium.Core;
using Librarium.Json;
using Quartz.Core.Diagnostics;
using ReactiveUI;
using Serilog;
using Splat;

namespace Quartz.IDE
{
    /// <summary>
    /// Houses global application data and the entry point of the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the metadata for the application.
        /// </summary>
        public static Meta Metadata { get; } = new Meta();

        /// <summary>
        /// Gets the currently loaded preferences for the user.
        /// </summary>
        public static Preferences Preferences { get; set; }

        /// <summary>
        /// When the application ends from anywhere, the log needs to be closed and flushed.
        /// </summary>
        [Log("Quartz will now flush the logger and shut down.")]
        private static void OnExit(object sender, EventArgs args) => Log.CloseAndFlush();

        /// <summary>
        /// Serves as the application entry point.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="args">
        /// Contains the arguments pertaining to the application startup event.
        /// </param>
        private void AppStartup(object sender, StartupEventArgs args)
        {
            DefineDebugFlag();

            InitializeLogging();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

            EnterProgram();
        }

        /// <summary>
        /// Enters the program.
        /// </summary>
        [Log("Starting...", "S")]
        private static void EnterProgram()
        {
            UpdateManager.Initialize();

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            Metadata = JFile.Load<PreferencesFile>(Metadata.AppDataDirectory, "metadata.json").CreateModel();
            Metadata.Save();
            Preferences.Load();

            new MainWindow().Show();
        }

        /// <summary>
        /// Initializes the logging manager and logs the welcome messages.
        /// </summary>
        private static void InitializeLogging()
        {
            LogManager.Initialize();

            Log.Information("Welcome to Quartz IDE Version {Version}.", Metadata.Version);

            if (Metadata.IsDebug)
            {
                Log.Warning("This assembly is running in DEBUG mode.");
            }
        }

        /// <summary>
        /// Defines the flag that determines whether or not the program is running in debug mode.
        /// </summary>
        private static void DefineDebugFlag() =>
#if DEBUG
            Metadata.IsDebug = true;

#endif
    }
}
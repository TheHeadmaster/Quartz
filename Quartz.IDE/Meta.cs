using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Librarium.Core;
using Quartz.IDE.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE
{
    /// <summary>
    /// Houses the metadata for the application.
    /// </summary>
    public class Meta : ReactiveObject
    {
        /// <summary>
        /// Gets the Directory for Quartz that lies inside of the user's AppData directory.
        /// </summary>
        public string AppDataDirectory => Path.Combine(AppMeta.AppDataDirectory, "Quartz");

        /// <summary>
        /// Gets the root directory that the program is contained in.
        /// </summary>
        public string AssemblyDirectory => AppMeta.AssemblyDirectory;

        /// <summary>
        /// Gets or sets the current project.
        /// </summary>
        [Reactive]
        public Project CurrentProject { get; set; }

        /// <summary>
        /// Gets whether the program is running in debug mode or not.
        /// </summary>
        public bool IsDebug { get; set; } = false;

        /// <summary>
        /// Gets the assembly version of the entry assembly.
        /// </summary>
        public Version Version => Assembly.GetEntryAssembly().GetName().Version;
    }
}
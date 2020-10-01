using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DynamicData;
using DynamicData.Binding;
using Librarium.Core;
using Quartz.IDE.ObjectModel;
using Quartz.IDE.UI;
using Quartz.IDE.ViewModels;
using Quartz.IDE.ViewModels.Pages;
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
        public Project? CurrentProject { get; set; }

        /// <summary>
        /// Gets whether the program is running in debug mode or not.
        /// </summary>
        public bool IsDebug { get; set; } = false;

        /// <summary>
        /// Gets or sets whether a debugger instance is currently running or not.
        /// </summary>
        [Reactive]
        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// Gets or sets the open pages in the workspace.
        /// </summary>
        public ObservableCollectionExtended<PageViewModel> Pages { get; } = new ObservableCollectionExtended<PageViewModel>();

        /// <summary>
        /// Gets or sets the selected open page in the workspace.
        /// </summary>
        [Reactive]
        public PageViewModel? SelectedPage { get; set; }

        /// <summary>
        /// Gets or sets the color of the status bar.
        /// </summary>
        [Reactive]
        public StatusBarColor StatusBarColor { get; set; } = StatusBarColor.Idle;

        /// <summary>
        /// Gets or sets the message displayed in the status bar.
        /// </summary>
        [Reactive]
        public string StatusBarMessage { get; set; } = "Ready";

        /// <summary>
        /// Gets the assembly version of the entry assembly.
        /// </summary>
        public Version Version => Assembly.GetEntryAssembly()!.GetName()!.Version!;

        /// <summary>
        /// Creates a new <see cref="Meta"/>.
        /// </summary>
        public Meta()
        {
            this.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }
                    Environment.CurrentDirectory = x.FilePath;
                });
        }

        /// <summary>
        /// Opens the specified page.
        /// </summary>
        /// <param name="type">
        /// The page to open.
        /// </param>
        public Task OpenPageAsync(Type type)
        {
            PageViewModel? newViewModel = this.Pages.FirstOrDefault(x => x.GetType() == type);
            if (newViewModel is null)
            {
                newViewModel = (PageViewModel?)Activator.CreateInstance(type);
                if (newViewModel is null) { return Task.CompletedTask; }
                this.Pages.Add(newViewModel);
            }

            this.SelectedPage = newViewModel;
            return Task.CompletedTask;
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using Librarium.Core;
using Librarium.Json;
using Microsoft.WindowsAPICodePack.Dialogs;
using Quartz.IDE.Json;
using Quartz.IDE.ObjectModel;
using Quartz.IDE.UI;
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
        public Project? CurrentProject { get; private set; }

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
        /// Gets whether or not a task has a known completion percentage.
        /// </summary>
        [Reactive]
        public bool ProgressIsIndeterminate { get; set; } = true;

        /// <summary>
        /// Gets the percentage progress that a task has made towards completing.
        /// </summary>
        [Reactive]
        public double ProgressPercentage { get; set; } = 0;

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
                    if (x is null || x.FilePath.IsNullOrWhiteSpace()) { return; }
                    Environment.CurrentDirectory = x.FilePath;
                });
        }

        /// <summary>
        /// Opens a common file dialog.
        /// </summary>
        /// <returns>
        /// The path selected by the user.
        /// </returns>
        private string? CommonFileDialog()
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog
            {
                Title = "Select a project file to open...",
                IsFolderPicker = false,
                InitialDirectory = @"C:\",
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = @"C:\",
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            dlg.Filters.Add(new CommonFileDialogFilter("JSON Project Files", "*.json"));

            return dlg.ShowDialog() == CommonFileDialogResult.Ok ? dlg.FileName : null;
        }

        /// <summary>
        /// Opens the project from a dialog.
        /// </summary>
        /// <returns>
        /// 0 if successful. Any other number indicates a problem occurred.
        /// </returns>
        private async Task<int> OpenFromDialog(bool? saveBeforeClosing)
        {
            string? path = this.CommonFileDialog();
            return path.IsNullOrWhiteSpace() ? 1 : await this.OpenFromPath(path!, saveBeforeClosing);
        }

        /// <summary>
        /// Opens the project from a path.
        /// </summary>
        /// <returns>
        /// 0 if successful. Any other number indicates a problem occurred.
        /// </returns>
        private async Task<int> OpenFromPath(string path, bool? saveBeforeClosing)
        {
            Task<int>? prepareProject = await Observable.Start(() => this.PrepareProject(Path.Combine(path, "Project.json"), saveBeforeClosing), RxApp.TaskpoolScheduler);
            return await prepareProject;
        }

        private async Task<int> PrepareProject(string path, bool? saveBeforeClosing = true)
        {
            await this.ChangeToWaitingStatus($"Loading project file from {path}...", StatusBarColor.Processing);
            if (path is null)
            {
                await this.ChangeToSimpleStatus("No project to open. Path was null.", StatusBarColor.Error);
                return 1;
            }
            ProjectFile file = JFile.Load<ProjectFile>(path);
            if (file is null)
            {
                await this.ChangeToSimpleStatus("No project to open. File does not exist.", StatusBarColor.Error);
                return 1;
            }
            if (file.Version is null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show($"This project file was made using 1.1, and is not compatible with the current release ({AppMeta.CurrentVersion}). Your project will not be harmed, but if you want to open it, you will have to revert back to 1.1.", "Incompatible Project", MessageBoxButton.OK);
                await this.ClearStatus();
                return 1;
            }
            if (App.Metadata.CurrentProject is { }) { await App.Metadata.CurrentProject.Close(saveBeforeClosing ?? true); }
            Project project = file.CreateModel();
            await this.ChangeCurrentProject(project);
            await project.Load();
            App.Preferences.Save();
            await this.ClearStatus();
            return 0;
        }

        /// <summary>
        /// Changes the current project to the project specified.
        /// </summary>
        /// <param name="project">
        /// The project to change to.
        /// </param>
        public async Task ChangeCurrentProject(Project project)
        => await Observable.Start(() =>
        {
            this.CurrentProject = project;
            App.Preferences.RecentlyOpenedProjects.AddOrUpdate(new RecentItem(project.Name ?? "", project.FilePath, DateTime.Now));
        }, RxApp.MainThreadScheduler);

        /// <summary>
        /// Changes the status information.
        /// </summary>
        /// <param name="statusMessage">
        /// The status message to change to.
        /// </param>
        /// <param name="progressPercentage">
        /// The progress completion percentage.
        /// </param>
        /// <param name="IsIndeterminate">
        /// Whether the progress bar is indeterminate or not.
        /// </param>
        /// <param name="color">
        /// The status bar color to change to.
        /// </param>
        public async Task ChangeStatus(string statusMessage, double progressPercentage, bool IsIndeterminate, StatusBarColor color)
        => await Observable.Start(() =>
        {
            App.Metadata.StatusBarMessage = statusMessage;
            App.Metadata.StatusBarColor = color;
            App.Metadata.ProgressIsIndeterminate = IsIndeterminate;
            App.Metadata.ProgressPercentage = progressPercentage;
        }, RxApp.MainThreadScheduler);

        /// <summary>
        /// Changes the status to a determinate progress status.
        /// </summary>
        /// <param name="statusMessage">
        /// The message to display.
        /// </param>
        /// <param name="color">
        /// The status bar color to change to.
        /// </param>
        public async Task ChangeToProgressStatus(string statusMessage, StatusBarColor color) => await this.ChangeStatus(statusMessage, 1, false, color);

        /// <summary>
        /// Changes the status to a non-progress status.
        /// </summary>
        /// <param name="statusMessage">
        /// The message to display.
        /// </param>
        /// <param name="color">
        /// The status bar color to change to.
        /// </param>
        public async Task ChangeToSimpleStatus(string statusMessage, StatusBarColor color) => await this.ChangeStatus(statusMessage, 0, false, color);

        /// <summary>
        /// Changes the status to an indeterminate waiting status.
        /// </summary>
        /// <param name="statusMessage">
        /// The message to display.
        /// </param>
        /// <param name="color">
        /// The status bar color to change to.
        /// </param>
        public async Task ChangeToWaitingStatus(string statusMessage, StatusBarColor color) => await this.ChangeStatus(statusMessage, 1, true, color);

        /// <summary>
        /// Clears the current project.
        /// </summary>
        public async Task ClearCurrentProject() => await Observable.Start(() =>
        {
            this.CurrentProject = null;
        }, RxApp.MainThreadScheduler);

        /// <summary>
        /// Clears any ongoing status messages.
        /// </summary>
        public async Task ClearStatus() => await this.ChangeStatus("Ready", 0, false, StatusBarColor.Idle);

        /// <summary>
        /// Closes the program.
        /// </summary>
        public async Task Close()
        {
            bool? saveResult = await this.PromptToSave();
            if (saveResult is null)
            {
                return;
            }
            else if (saveResult is true)
            {
                this.CurrentProject!.Save();
            }
            Application.Current.Shutdown();
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

        /// <summary>
        /// Opens a project from a path. If no path is specified, or the path is null or whitespace,
        /// a file dialog is opened to select a project.
        /// </summary>
        /// <param name="path">
        /// The optional path to the project.
        /// </param>
        public async Task OpenProject(string? path = null)
        {
            bool? saveResult = await this.PromptToSave();
            int success = path.IsNullOrWhiteSpace() ? await this.OpenFromDialog(saveResult) : await this.OpenFromPath(path!, saveResult);
            if (success == 0)
            {
                App.Metadata.CurrentProject!.IsSaved = true;
            }
        }

        /// <summary>
        /// Checks if a project is open, then prompts the user to save any unsaved changes.
        /// </summary>
        /// <returns>
        /// True if the user wants to save, or false if they don't. Returns null if the user cancels.
        /// </returns>
        public Task<bool?> PromptToSave()
        {
            if (App.Metadata.CurrentProject is { } && !App.Metadata.CurrentProject.IsSaved && !App.Metadata.CurrentProject.AreItemsSaved)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(
                    $"Changes have been made to {App.Metadata.CurrentProject.Name}. Would you like to save these changes before closing the project?",
                    "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                return result switch
                {
                    MessageBoxResult.Yes => Task.FromResult<bool?>(true),
                    MessageBoxResult.No => Task.FromResult<bool?>(false),
                    _ => Task.FromResult<bool?>(null),
                };
            }
            else
            {
                return Task.FromResult<bool?>(false);
            }
        }
    }
}
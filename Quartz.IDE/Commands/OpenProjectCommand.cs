using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DynamicData;
using Librarium.Commands;
using Librarium.Core;
using Librarium.Json;
using Microsoft.WindowsAPICodePack.Dialogs;
using Quartz.IDE.Json;

namespace Quartz.IDE.Commands
{
    /// <summary>
    /// Opens a project.
    /// </summary>
    public class OpenProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenProjectCommand();

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

        private int OpenFromDialog()
        {
            string? path = this.CommonFileDialog();
            return path.IsNullOrWhiteSpace() ? 1 : this.OpenFromPath(path!);
        }

        private int OpenFromPath(string path)
        {
            bool? saveBeforeClosing = true;
            if (App.Metadata.CurrentProject is { } && !App.Metadata.CurrentProject.IsSaved)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show(
                    $"Changes have been made to {App.Metadata.CurrentProject.Name}. Would you like to save these changes before closing the project?",
                    "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                saveBeforeClosing = result switch
                {
                    MessageBoxResult.Yes => true,
                    MessageBoxResult.No => false,
                    _ => null,
                };
                if (saveBeforeClosing is null) { return 1; }
            }

            return this.PrepareProject(path, saveBeforeClosing);
        }

        private int PrepareProject(string path, bool? saveBeforeClosing = true)
        {
            if (path is null) { return 1; }
            ProjectFile file = JFile.Load<ProjectFile>(path, "Project.json");
            if (file is null) { return 1; }
            if (file.Version is null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show($"This project file was made using 1.1, and is not compatible with the current release ({AppMeta.CurrentVersion}). Your project will not be harmed, but if you want to open it, you will have to revert back to 1.1.", "Incompatible Project", MessageBoxButton.OK);
                return 1;
            }
            if (App.Metadata.CurrentProject is { }) { App.Metadata.CurrentProject.Close(saveBeforeClosing ?? true).Wait(); }
            App.Metadata.CurrentProject = file.CreateModel();
            App.Preferences.RecentlyOpenedProjects.AddOrUpdate(
                new RecentItem(
                    App.Metadata.CurrentProject.Name ?? "",
                    App.Metadata.CurrentProject.FilePath,
                    DateTime.Now));
            App.Metadata.CurrentProject.Load();
            App.Preferences.Save();
            return 0;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            int success = 0;
            if (parameter is string path)
            {
                success = this.OpenFromPath(path);
            }
            else if (parameter is null)
            {
                success = this.OpenFromDialog();
            }
            if (success == 0)
            {
                App.Metadata.CurrentProject!.IsSaved = true;
            }
        }
    }
}
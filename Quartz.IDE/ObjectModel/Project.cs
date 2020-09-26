using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DynamicData;
using Librarium.Json;
using Quartz.Core.ObjectModel;
using Quartz.Core.ObjectModel.Attributes;
using Quartz.IDE.Json;
using Quartz.IDE.ViewModels;
using ReactiveUI.Fody.Helpers;
using PersistentEntity;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz project.
    /// </summary>
    public class Project : SaveableObject, IModelToFile
    {
        public Connection Connection => Connection.Create()
                    .AllowMARS()
                    .UseWindowsAuthentication()
                    .AttachDbFilename(Path.Combine(this.FileName, "Data.mdf"))
                    .UseSqlExpress()
                    .Build();

        public string FileName { get; set; }

        public string FilePath { get; set; }

        /// <summary>
        /// The name of the project.
        /// </summary>
        [Reactive]
        [Memento]
        public string Name { get; set; }

        /// <summary>
        /// The version of Quartz that the project was made with.
        /// </summary>
        [Reactive]
        [Memento]
        public Version Version { get; set; }

        public void Close(bool saveBeforeClosing)
        {
            if (saveBeforeClosing)
            {
                ProjectFile file = new ProjectFile
                {
                    FileName = "Project.json",
                    FilePath = this.FilePath
                };
                file.PopulateFile(this);
                file.Save();
            }
            App.Metadata.CurrentProject = null;
        }

        public void Load()
        {
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            App.Preferences.RecentlyOpenedProjects.AddOrUpdate(new RecentItem(this.Name, this.FilePath, DateTime.Now));
            App.Preferences.Save();
            this.IsSaved = true;
        }

        /// <summary>
        /// Creates a new json data model, populates it with this project's data, and saves it to disk.
        /// </summary>
        public void Save()
        {
            ProjectFile file = new ProjectFile();
            file.PopulateFile(this);
            file.Save();
            this.IsSaved = true;
        }
    }
}
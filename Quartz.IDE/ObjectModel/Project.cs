using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Librarium.Json;
using Quartz.IDE.Json;
using Quartz.IDE.ObjectModel.Attributes;
using Quartz.IDE.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz project.
    /// </summary>
    public class Project : JFileDocumentControl
    {
        public override string Header => this.Name;

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

        public override DocumentControlViewModel CreateViewModel() => new ProjectViewModel { Model = this };

        public void Load()
        {
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            this.IsSaved = true;
        }

        /// <summary>
        /// Creates a new json data model, populates it with this project's data, and saves it to disk.
        /// </summary>
        public override void Save()
        {
            ProjectFile file = new ProjectFile();
            file.PopulateFile(this);
            file.Save();
            this.IsSaved = true;
        }
    }
}
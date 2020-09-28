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
using System.Text.Json.Serialization;
using ReactiveUI;
using System.Reactive.Linq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz project.
    /// </summary>
    public class Project : SaveableObject, IModelToFile
    {
        /// <summary>
        /// Gets whether all project items are saved.
        /// </summary>
        public bool AreItemsSaved { [ObservableAsProperty]get; }

        /// <summary>
        /// Represents a connection to this project's database.
        /// </summary>
        public Connection Connection => Connection.Create()
                            .AllowMARS()
                    .UseWindowsAuthentication()
                    .AttachDbFilename(Path.Combine(this.FilePath, "Data.mdf"))
                    .UseInitialCatalog(Path.Combine(this.FilePath, this.FileName))
                    .UseLocalDB()
                    .Build();

        /// <summary>
        /// A list of all <see cref="ElementMatchup"/> s in the database.
        /// </summary>
        [Reactive]
        [Memento]
        public SourceCache<ElementMatchup, int> ElementMatchups { get; set; } = new SourceCache<ElementMatchup, int>(x => x.ID);

        /// <summary>
        /// A list of all <see cref="Element"/> s in the database.
        /// </summary>
        [Reactive]
        [Memento]
        public SourceCache<Element, int> Elements { get; set; } = new SourceCache<Element, int>(x => x.ID);

        /// <summary>
        /// The name of the <see cref="JFile"/> on disk.
        /// </summary>
        public string FileName { get; set; } = "";

        /// <summary>
        /// The path to the <see cref="JFile"/> on disk.
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// The name of the project.
        /// </summary>
        [Reactive]
        [Memento]
        public string? Name { get; set; } = "";

        /// <summary>
        /// The version of Quartz that the project was made with.
        /// </summary>
        [Reactive]
        [Memento]
        public Version? Version { get; set; }

        /// <summary>
        /// Creates a new <see cref="Project"/>.
        /// </summary>
        public Project()
        {
            this.Elements.Connect()
                .WhenValueChanged(x => x.IsSaved)
                .Select(x =>
                {
                    return this.Elements.Items.All(x => x.IsSaved);
                })
                .ToPropertyEx(this, x => x.AreItemsSaved);
        }

        /// <summary>
        /// Closes the project with an option to save before closing.
        /// </summary>
        /// <param name="saveBeforeClosing">
        /// Saves before closing if <see langword="true"/>.
        /// </param>
        public async Task Close(bool saveBeforeClosing)
        {
            if (saveBeforeClosing)
            {
                await this.SaveAllAsync();
            }
            SaveableObjects.Clear();
            App.Metadata.CurrentProject = null;
        }

        /// <summary>
        /// Loads the project and all of its associated data objects.
        /// </summary>
        public void Load()
        {
            using (DatabaseTransaction<QuartzContext> transaction = new DatabaseTransaction<QuartzContext>(this.Connection))
            {
                this.Elements.AddOrUpdate(transaction.GetAll<Element>());
                this.ElementMatchups.AddOrUpdate(transaction.GetAll<ElementMatchup>());
            }
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            App.Preferences.RecentlyOpenedProjects.AddOrUpdate(new RecentItem(this.Name ?? "", this.FilePath, DateTime.Now));
            App.Preferences.Save();
            this.IsSaved = true;
        }

        /// <summary>
        /// Saves the <see cref="JFile"/> to disk.
        /// </summary>
        public void Save()
        {
            ProjectFile file = new ProjectFile();
            file.PopulateFile(this);
            file.Save();
        }

        /// <summary>
        /// Saves the project and all of its associated data objects.
        /// </summary>
        public async Task SaveAllAsync() => await SaveableObject.SaveAllAsync(this.Connection);

        /// <summary>
        /// Saves the <see cref="JFile"/> to disk.
        /// </summary>
        public override Task SaveAsync(Connection? connection = null)
        {
            this.Save();
            this.IsSaved = true;
            return Task.CompletedTask;
        }
    }
}
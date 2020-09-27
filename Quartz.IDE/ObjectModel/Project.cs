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

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz project.
    /// </summary>
    public class Project : SaveableObject, IModelToFile
    {
        public bool AreItemsSaved { [ObservableAsProperty]get; }

        public Connection Connection => Connection.Create()
                            .AllowMARS()
                    .UseWindowsAuthentication()
                    .AttachDbFilename(Path.Combine(this.FilePath, "Data.mdf"))
                    .UseInitialCatalog(Path.Combine(this.FilePath, this.FileName))
                    .UseLocalDB()
                    .Build();

        public SourceCache<ElementMatchup, int> ElementMatchups { get; set; } = new SourceCache<ElementMatchup, int>(x => x.ID);

        [Reactive]
        [Memento]
        public SourceCache<Element, int> Elements { get; set; } = new SourceCache<Element, int>(x => x.ID);

        public string FileName { get; set; }

        public string FilePath { get; set; }

        /// <summary>
        /// The name of the project.
        /// </summary>
        [Reactive]
        [Memento]
        public string Name { get; set; }

        public SourceList<SaveableObject> SaveableObjects { get; } = new SourceList<SaveableObject>();

        /// <summary>
        /// The version of Quartz that the project was made with.
        /// </summary>
        [Reactive]
        [Memento]
        public Version Version { get; set; }

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
            using (DatabaseTransaction<QuartzContext> transaction = new DatabaseTransaction<QuartzContext>(this.Connection))
            {
                this.Elements.AddOrUpdate(transaction.GetAll<Element>());
                this.ElementMatchups.AddOrUpdate(transaction.GetAll<ElementMatchup>());
            }
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            App.Preferences.RecentlyOpenedProjects.AddOrUpdate(new RecentItem(this.Name, this.FilePath, DateTime.Now));
            App.Preferences.Save();
            this.IsSaved = true;
        }

        /// <summary>
        /// Saves the project and all of its associated data objects.
        /// </summary>
        public override void Save()
        {
            IEnumerable<IDatabaseObject> dbObjects = this.SaveableObjects.Items.Where(x => x is IDatabaseObject).Cast<IDatabaseObject>();

            using (DatabaseTransaction<QuartzContext> transaction = new DatabaseTransaction<QuartzContext>(this.Connection))
            {
                foreach (IDatabaseObject dbObject in dbObjects)
                {
                    transaction.AddOrUpdate(dbObject);
                }
                transaction.SaveChanges();
            }
            dbObjects.Cast<SaveableObject>().ToList().ForEach(x => { x.IsSaved = true; this.SaveableObjects.Remove(x); });
            foreach (SaveableObject nonDbObject in this.SaveableObjects.Items)
            {
                nonDbObject.Save();
                nonDbObject.IsSaved = true;
            }
            this.SaveableObjects.Clear();

            ProjectFile file = new ProjectFile();
            file.PopulateFile(this);
            file.Save();
            this.IsSaved = true;
        }
    }
}
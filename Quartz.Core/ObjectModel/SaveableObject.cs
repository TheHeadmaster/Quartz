using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DynamicData;
using PersistentEntity;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// A <see cref="ReactiveObject"/> that tracks relevant saveable changes.
    /// </summary>
    public abstract class SaveableObject : ReactiveObject
    {
        /// <summary>
        /// Gets a list of all <see cref="SaveableObject"/> s.
        /// </summary>
        public static SourceList<SaveableObject> SaveableObjects { get; } = new SourceList<SaveableObject>();

        /// <summary>
        /// Gets or sets whether or not this object matches the saved data model on disk.
        /// </summary>
        [Reactive]
        [NotMapped]
        public bool IsSaved { get; set; } = true;

        /// <summary>
        /// Creates a new <see cref="SaveableObject"/>.
        /// </summary>
        public SaveableObject()
        {
            SaveableObjects.Add(this);
            this.PropertyChanged += this.ObservableObjectPropertyChanged;
        }

        private void EvaluateSavableChanges(string propertyName)
        {
            List<PropertyInfo> properties = this.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(MementoAttribute), true)).ToList();
            if (properties.Any(x => x.Name == propertyName))
            {
                this.IsSaved = false;
            }
        }

        private void ObservableObjectPropertyChanged(object sender, PropertyChangedEventArgs args) => this.EvaluateSavableChanges(args.PropertyName);

        /// <summary>
        /// Saves all unsaved objects asynchronously, then marks them as saved.
        /// </summary>
        /// <param name="connection">
        /// </param>
        /// <returns>
        /// </returns>
        public static async Task SaveAllAsync(Connection connection)
        {
            IEnumerable<SaveableObject> unsavedFileObjects = SaveableObjects.Items.Where(x => !x.IsSaved && !(x is IDatabaseObject));
            IEnumerable<IDatabaseObject> unsavedDbObjects = SaveableObjects.Items.Where(x => !x.IsSaved && x is IDatabaseObject).Cast<IDatabaseObject>();
            foreach (SaveableObject unsavedFileObject in unsavedFileObjects)
            {
                await unsavedFileObject.SaveAsync(connection);
            }
            if (unsavedDbObjects.Count() > 0)
            {
                using (DatabaseTransaction<QuartzContext> transaction = new DatabaseTransaction<QuartzContext>(connection))
                {
                    foreach (IDatabaseObject unsavedDbObject in unsavedDbObjects)
                    {
                        await transaction.AddOrUpdateAsync(unsavedDbObject);
                    }
                    await transaction.SaveChangesAsync();
                }
                foreach (SaveableObject unsavedDbObject in SaveableObjects.Items.Where(x => !x.IsSaved && x is IDatabaseObject))
                {
                    unsavedDbObject.IsSaved = true;
                }
            }
        }

        /// <summary>
        /// Removes this <see cref="SaveableObject"/> from the list of tracked <see
        /// cref="SaveableObject"/> s.
        /// </summary>
        public void Remove() => SaveableObjects.Remove(this);

        /// <summary>
        /// Saves the object asynchronously, and marks it as saved.
        /// </summary>
        /// <param name="connection">
        /// An optional connection to pass in.
        /// </param>
        public abstract Task SaveAsync(Connection? connection = null);
    }
}
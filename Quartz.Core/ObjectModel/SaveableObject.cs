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
    public abstract class SaveableObject : ReactiveObject
    {
        private static SourceList<SaveableObject> SaveableObjects { get; } = new SourceList<SaveableObject>();

        [Reactive]
        [NotMapped]
        public bool IsSaved { get; set; } = true;

        public SaveableObject()
        {
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

        public static async Task SaveAll(Connection connection)
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
            }
        }

        public abstract Task SaveAsync(Connection connection);
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public abstract class SaveableObject : ReactiveObject
    {
        [Reactive]
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
    }
}
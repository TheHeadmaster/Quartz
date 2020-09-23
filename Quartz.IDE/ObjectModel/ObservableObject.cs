using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Quartz.IDE.ObjectModel.Attributes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ObjectModel
{
    public abstract class ObservableObject : ReactiveObject
    {
        public Visibility DocumentAsteriskVisibility { [ObservableAsProperty] get; }

        [Reactive]
        public bool IsSaved { get; set; } = true;

        public ObservableObject()
        {
            this.PropertyChanged += this.ObservableObjectPropertyChanged;
            this.WhenAnyValue(x => x.IsSaved)
                .Select(x => x ? Visibility.Collapsed : Visibility.Visible)
                .ToPropertyEx(this, x => x.DocumentAsteriskVisibility);
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
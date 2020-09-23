using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;
using Quartz.IDE.ObjectModel;
using ReactiveUI;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// Represents a ViewModel for a document control.
    /// </summary>
    public abstract class DocumentControlViewModel : ReactiveObject
    {
        public abstract DocumentControl GetModel();
    }

    /// <summary>
    /// Represents a ViewModel for a document control.
    /// </summary>
    /// <typeparam name="T">
    /// The document control type that this derived ViewModel belongs to.
    /// </typeparam>
    public abstract class DocumentControlViewModel<T> : DocumentControlViewModel where T : DocumentControl
    {
        [SuppressPropertyChangedWarnings]
        public abstract T Model { get; set; }

        public override DocumentControl GetModel() => this.Model;
    }
}
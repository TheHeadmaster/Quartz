using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;
using Quartz.IDE.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels.Pages
{
    /// <summary>
    /// Represents a ViewModel for a page.
    /// </summary>
    public abstract class PageViewModel : ReactiveObject
    {
        public string Header { [ObservableAsProperty]get; }

        public string Identifier { [ObservableAsProperty]get; }
    }
}
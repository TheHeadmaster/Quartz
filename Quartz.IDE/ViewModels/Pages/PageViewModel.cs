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
        /// <summary>
        /// Determines whats displayed in the <see cref="PageViewModel"/>'s tab header.
        /// </summary>
        public string Header { [ObservableAsProperty]get; } = "";

        /// <summary>
        /// A unique identifier which distinguishes it from other <see cref="PageViewModel"/> s.
        /// </summary>
        public string Identifier { [ObservableAsProperty]get; } = "";
    }
}
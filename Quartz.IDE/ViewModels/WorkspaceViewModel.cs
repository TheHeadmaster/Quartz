using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using Quartz.IDE.Controls;
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// ViewModel for the <see cref="Workspace"/> control.
    /// </summary>
    public class WorkspaceViewModel : ReactiveObject
    {
        /// <summary>
        /// Gets a list of open Pages.
        /// </summary>
        public ObservableCollectionExtended<PageViewModel> Pages => App.Metadata.Pages;
    }
}
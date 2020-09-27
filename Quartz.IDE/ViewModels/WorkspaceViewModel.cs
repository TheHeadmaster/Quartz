using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    public class WorkspaceViewModel : ReactiveObject
    {
        public ObservableCollectionExtended<PageViewModel> Pages => App.Metadata.Pages;
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    public class MainMenuViewModel : ReactiveObject
    {
        private readonly ReadOnlyObservableCollection<RecentItem> recentlyOpenedProjects;

        public ReadOnlyObservableCollection<RecentItem> RecentlyOpenedProjects => this.recentlyOpenedProjects;

        public MainMenuViewModel()
        {
            App.Preferences.RecentlyOpenedProjects
                .Connect()
                .Sort(SortExpressionComparer<RecentItem>.Descending(x => x.Timestamp))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.recentlyOpenedProjects)
                .Subscribe();
        }
    }
}
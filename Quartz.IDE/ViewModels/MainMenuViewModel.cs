using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AvalonDock.Layout;
using DynamicData;
using DynamicData.Binding;
using Quartz.IDE.Controls;
using Quartz.IDE.UI;
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// The ViewModel for the <see cref="MainMenu"/> control.
    /// </summary>
    public class MainMenuViewModel : ReactiveObject
    {
        private readonly ReadOnlyObservableCollection<RecentItem> recentlyOpenedProjects;

        private string openingPath;

        /// <summary>
        /// Closes the program.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Close { get; } = ReactiveCommand.Create(() => Application.Current.Shutdown());

        /// <summary>
        /// Opens the elements page.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenElementsPage { get; }

        /// <summary>
        /// Opens a project.
        /// </summary>
        public ReactiveCommand<string, Unit> OpenProject { get; }

        /// <summary>
        /// Opens the tiles page.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenTilesPage { get; }

        /// <summary>
        /// Gets a list of recently opened projects.
        /// </summary>
        public ReadOnlyObservableCollection<RecentItem> RecentlyOpenedProjects => this.recentlyOpenedProjects;

        /// <summary>
        /// Updates the status.
        /// </summary>
        public ReactiveCommand<Unit, Unit> UpdateStatus { get; }

        /// <summary>
        /// Creates a new <see cref="MainMenuViewModel"/>.
        /// </summary>
        public MainMenuViewModel()
        {
            App.Preferences.RecentlyOpenedProjects
                .Connect()
                .Sort(SortExpressionComparer<RecentItem>.Descending(x => x.Timestamp))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.recentlyOpenedProjects)
                .Subscribe();

            IObservable<bool> canOpenPage = App.Metadata.WhenAnyValue(x => x.IsRunning, x => x.CurrentProject, (x, y) => x)
                .Select(isRunning => !isRunning && App.Metadata.CurrentProject is { });

            this.OpenElementsPage = ReactiveCommand.CreateFromTask(x => App.Metadata.OpenPageAsync(typeof(ElementsViewModel)), canOpenPage);
            this.OpenTilesPage = ReactiveCommand.CreateFromTask(x => App.Metadata.OpenPageAsync(typeof(TilesViewModel)), canOpenPage);
            this.OpenProject = ReactiveCommand.CreateFromTask<string>(x => App.Metadata.OpenProject(x));
        }
    }
}
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

        /// <summary>
        /// Closes the program.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Close { get; } = ReactiveCommand.Create(() => Application.Current.Shutdown());

        /// <summary>
        /// Opens the elements page.
        /// </summary>
        public ReactiveCommand<Unit, Unit> OpenElementsPage { get; }

        /// <summary>
        /// Gets a list of recently opened projects.
        /// </summary>
        public ReadOnlyObservableCollection<RecentItem> RecentlyOpenedProjects => this.recentlyOpenedProjects;

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
                .Select(isRunning =>
                {
                    Log.Information("Reevaluating CanExecute for OpenPageAsync");
                    Log.Information("IsRunning = {IsRunning}, CurrentProject = {CurrentProject}", isRunning, App.Metadata.CurrentProject is { });
                    Log.Information("CanExecute: {CanExecute}", !isRunning && App.Metadata.CurrentProject is { });
                    return !isRunning && App.Metadata.CurrentProject is { };
                });

            this.OpenElementsPage = ReactiveCommand.CreateFromTask(x => App.Metadata.OpenPageAsync(typeof(ElementsViewModel)),
                canOpenPage);
        }
    }
}
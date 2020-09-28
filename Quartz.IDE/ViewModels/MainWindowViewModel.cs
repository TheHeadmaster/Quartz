using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// ViewModel for the Main Window.
    /// </summary>
    public class MainWindowViewModel : ReactiveObject
    {
        /// <summary>
        /// Closes Quartz.
        /// </summary>
        public ReactiveCommand<Unit, Unit> Close { get; } = ReactiveCommand.Create(() =>
        Application.Current.Shutdown());

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string Title { [ObservableAsProperty] get; } = "";

        /// <summary>
        /// Creates a new <see cref="MainWindowViewModel"/>.
        /// </summary>
        public MainWindowViewModel()
        {
            App.Metadata.WhenAnyValue(x => x.CurrentProject, x => x.CurrentProject!.IsSaved, x => x.CurrentProject!.AreItemsSaved, (x, y, z) => x)
               .Select(x => x is null ? "Quartz IDE" : $"Quartz IDE - {x.Name}{(!x.IsSaved || !x.AreItemsSaved ? "*" : null)}")
               .ToPropertyEx(this, x => x.Title, "Quartz IDE");
        }
    }
}
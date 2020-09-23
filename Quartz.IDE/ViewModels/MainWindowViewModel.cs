using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// ViewModel for the Main Window.
    /// </summary>
    public class MainWindowViewModel : ReactiveObject
    {
        public string Title { [ObservableAsProperty] get; }

        public MainWindowViewModel()
        {
            // App.Metadata.WhenAnyValue(x => x.CurrentProject, x => x.CurrentProject.IsSaved, (x,
            // y) => x) .Select(x => x is null ? "Quartz IDE" : $"Quartz IDE - {x.Name}{(!x.IsSaved
            // ? "*" : null)}") .ToPropertyEx(this, x => x.Title, "Quartz IDE");
        }
    }
}
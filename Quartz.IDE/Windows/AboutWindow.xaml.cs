using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Librarium.WPF.Windows;
using Quartz.IDE.Converters;
using Quartz.IDE.UI;
using Quartz.IDE.ViewModels;
using ReactiveUI;

namespace Quartz.IDE.Windows
{
    /// <summary>
    /// The <see cref="AboutWindow"/> shows information about the program's version, copyright
    /// information, and other credits.
    /// </summary>
    public partial class AboutWindow : BorderlessReactiveWindow<AboutWindowViewModel>
    {
        public AboutWindow()
        {
            this.InitializeComponent();

            this.ViewModel = new AboutWindowViewModel();

            // When this view is activated, bind controls to the View Model, and dispose these
            // bindings with the view.
            this.WhenActivated(dispose =>
            {
                // Version Text
                this.OneWayBind(this.ViewModel,
                    vm => vm.Version,
                    view => view.VersionText.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.Changelog,
                    view => view.Changelog.Document,
                    vmToViewConverterOverride: new TextToFlowDocumentConverter())
                .DisposeWith(dispose);
            });

            this.Closed += this.AboutWindow_Closed;
            App.Metadata.StatusBarColor = StatusBarColor.Processing;
            App.Metadata.StatusBarMessage = "Window Modal";
        }

        private void AboutWindow_Closed(object sender, EventArgs args)
        {
            App.Metadata.StatusBarColor = StatusBarColor.Idle;
            App.Metadata.StatusBarMessage = "Ready";
        }
    }
}
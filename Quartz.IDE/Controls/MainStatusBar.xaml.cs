#nullable disable

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quartz.IDE.ViewModels;
using ReactiveUI;

namespace Quartz.IDE.Controls
{
    /// <summary>
    /// Interaction logic for MainStatusBar.xaml
    /// </summary>
    public partial class MainStatusBar : ReactiveUserControl<MainStatusBarViewModel>
    {
        public MainStatusBar()
        {
            this.InitializeComponent();

            this.ViewModel = new MainStatusBarViewModel();

            this.WhenActivated(dispose =>
            {
                this.OneWayBind(this.ViewModel,
                    vm => vm.Metadata.StatusBarMessage,
                    view => view.StatusBarMessage.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.Metadata.StatusBarColor.Color,
                    view => view.StatusBarBorder.Background)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.Metadata.StatusBarColor.Foreground,
                    view => view.StatusBarMessage.Foreground)
                .DisposeWith(dispose);
            });
        }
    }
}
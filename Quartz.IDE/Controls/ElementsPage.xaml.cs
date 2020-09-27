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
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;

namespace Quartz.IDE.Controls
{
    /// <summary>
    /// Interaction logic for ElementsPage.xaml
    /// </summary>
    public partial class ElementsPage : ReactiveUserControl<ElementsViewModel>
    {
        public ElementsPage()
        {
            this.InitializeComponent();

            this.WhenActivated(dispose =>
            {
                this.ViewModel = (ElementsViewModel)this.DataContext;

                this.OneWayBind(this.ViewModel,
                    vm => vm.Elements,
                    view => view.ElementsList.ItemsSource)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedElement.Name,
                    view => view.NameText.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.AttackingMatchups,
                    view => view.AttackingListBox.ItemsSource)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.DefendingMatchups,
                    view => view.DefendingListBox.ItemsSource)
                .DisposeWith(dispose);
            });
        }
    }
}
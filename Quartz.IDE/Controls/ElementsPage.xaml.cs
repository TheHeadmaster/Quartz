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
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;

namespace Quartz.IDE.Controls
{
    /// <summary>
    /// Interaction logic for ElementsPage.xaml
    /// </summary>
    public partial class ElementsPage : ReactiveUserControl<ElementsViewModel>
    {
        /// <summary>
        /// Creates a new <see cref="ElementsPage"/>.
        /// </summary>
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
                    view => view.AttackingDataGrid.ItemsSource)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.DefendingMatchups,
                    view => view.DefendingDataGrid.ItemsSource)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedElement,
                    view => view.ElementsList.SelectedValue)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.NewElement,
                    view => view.NewElementButton)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.CreateAttackingMatchup,
                    view => view.NewAttackingMatchupButton)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.CreateDefendingMatchup,
                    view => view.NewDefendingMatchupButton)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.OffensiveRating,
                    view => view.OffensiveText.Text,
                    vmProperty => this.ConvertToText(vmProperty))
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.OffensiveRating,
                    view => view.OffensiveProgress.Value)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.DefensiveRating,
                    view => view.DefensiveText.Text,
                    vmProperty => this.ConvertToText(vmProperty))
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.DefensiveRating,
                    view => view.DefensiveProgress.Value)
                .DisposeWith(dispose);

                if (this.ElementsList.Items.Count > 0)
                {
                    this.ElementsList.SelectedIndex = 0;
                }
            });
        }

        private string ConvertToText(double value) => $"{value:F2}";
    }
}
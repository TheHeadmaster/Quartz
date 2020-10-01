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
    /// Interaction logic for TilesPage.xaml
    /// </summary>
    public partial class TilesPage : ReactiveUserControl<TilesViewModel>
    {
        /// <summary>
        /// Creates a new <see cref="TilesPage"/>.
        /// </summary>
        public TilesPage()
        {
            this.InitializeComponent();

            this.WhenActivated(dispose =>
            {
                this.ViewModel = (TilesViewModel)this.DataContext;

                this.OneWayBind(this.ViewModel,
                    vm => vm.Tiles,
                    view => view.TilesList.ItemsSource)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedTile.Name,
                    view => view.NameText.Text)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedTile.ImagePath,
                    view => view.ImagePathText.Text)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedTile,
                    view => view.TilesList.SelectedValue)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.CreateTile,
                    view => view.NewTileButton)
                .DisposeWith(dispose);

                if (this.TilesList.Items.Count > 0)
                {
                    this.TilesList.SelectedIndex = 0;
                }
            });
        }
    }
}
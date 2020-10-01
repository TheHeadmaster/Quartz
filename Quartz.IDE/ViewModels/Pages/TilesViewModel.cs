using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using Quartz.Core.ObjectModel;
using Quartz.IDE.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels.Pages
{
    /// <summary>
    /// The ViewModel for the <see cref="TilesPage"/> control
    /// </summary>
    public sealed class TilesViewModel : PageViewModel
    {
        private ReadOnlyObservableCollection<TileBase>? tiles;

        /// <summary>
        /// Gets whether all tiles are saved.
        /// </summary>
        public bool AreTilesSaved { [ObservableAsProperty]get; } = true;

        /// <summary>
        /// Creates a new tile.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateTile { get; }

        /// <summary>
        /// The currently selected tile.
        /// </summary>
        [Reactive]
        public TileBase? SelectedTile { get; set; }

        /// <summary>
        /// A list of all tiles.
        /// </summary>
        public ReadOnlyObservableCollection<TileBase>? Tiles => this.tiles;

        /// <summary>
        /// Creates a new <see cref="TilesViewModel"/>.
        /// </summary>
        public TilesViewModel()
        {
            this.CreateTile = ReactiveCommand.CreateFromTask(this.CreateTileAsync);

            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }
                    SaveableObject.SaveableObjects.Connect()
                    .WhenPropertyChanged(p => p.IsSaved)
                    .Select(p =>
                    {
                        return SaveableObject.SaveableObjects.Items.All(y =>
                        {
                            return !(y is TileBase) || y.IsSaved;
                        });
                    })
                    .ToPropertyEx(this, x => x.AreTilesSaved);
                });

            this.WhenAnyValue(x => x.AreTilesSaved)
                .Select(x =>
                {
                    return $"Tiles{(x ? string.Empty : "*")}";
                })
                .ToPropertyEx(this, x => x.Header);

            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }

                    // Connect to Tiles
                    IObservable<ISortedChangeSet<TileBase, int>>? tiles = App.Metadata.CurrentProject?.Tiles.Connect()
                        .Sort(SortExpressionComparer<TileBase>.Ascending(x => x.Name));

                    // Tiles Binding
                    tiles.ObserveOn(RxApp.MainThreadScheduler).Bind(out this.tiles).Subscribe();
                });
        }

        private Task CreateTileAsync()
        {
            string newTileName = "New Tile";
            int i = 2;
            while (this.Tiles.Any(x => x.Name == newTileName))
            {
                newTileName = $"New Tile {i}";
                i++;
            }
            TileBase tile = new TileBase
            {
                Name = newTileName,
                ID = this.Tiles.Select(x => x.ID).DefaultIfEmpty().Max() + 1,
                IsSaved = false
            };
            App.Metadata.CurrentProject?.Tiles.AddOrUpdate(tile);

            return Task.CompletedTask;
        }
    }
}
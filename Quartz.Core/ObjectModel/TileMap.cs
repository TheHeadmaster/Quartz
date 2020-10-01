using System;
using System.Collections.Generic;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class TileMap : SaveableDatabaseObject
    {
        /// <summary>
        /// Gets or sets the width and height of tiles on the map. Tiles will be resized to fit this size.
        /// </summary>
        [Memento]
        [Reactive]
        public int GridSize { get; set; }

        /// <summary>
        /// Gets or sets the height in tiles of the map.
        /// </summary>
        [Memento]
        [Reactive]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the layers of the map.
        /// </summary>
        [Memento]
        public List<MapLayer> Layers { get; set; } = null!;

        /// <summary>
        /// The name of the map. Must be unique.
        /// </summary>
        [Memento]
        [Reactive]
        public string Name { get; set; } = "";

        /// <summary>
        /// The title is what displays on world maps and entering the map. Multiple maps can have
        /// the same title. the same title.
        /// </summary>
        [Memento]
        [Reactive]
        public string Title { get; set; } = "";

        /// <summary>
        /// Gets or sets the width in tiles of the map.
        /// </summary>
        [Memento]
        [Reactive]
        public int Width { get; set; }
    }
}
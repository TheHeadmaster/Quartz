using System;
using System.Collections.Generic;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class MapLayer : SaveableDatabaseObject
    {
        [Memento]
        public List<Tile> Tiles { get; set; } = null!;

        [Memento]
        [Reactive]
        public int ZOrder { get; set; } = 0;
    }
}
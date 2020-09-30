using System;
using System.Collections.Generic;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class Tile : SaveableDatabaseObject
    {
        [Memento]
        [Reactive]
        public TileBase BasedOn { get; set; } = null!;

        [Memento]
        [Reactive]
        public CollisionMap? CollisionMap { get; set; }

        [Memento]
        [Reactive]
        public int? CollisionMapID { get; set; }

        [Memento]
        [Reactive]
        public string? ImagePath { get; set; }
    }
}
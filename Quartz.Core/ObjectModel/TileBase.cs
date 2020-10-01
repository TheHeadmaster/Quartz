using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class TileBase : SaveableDatabaseObject
    {
        [Memento]
        [Reactive]
        public CollisionMap? CollisionMap { get; set; }

        [Memento]
        [Reactive]
        public int? CollisionMapID { get; set; }

        [Memento]
        [Reactive]
        public string? ImagePath { get; set; }

        public string ImageSource { [ObservableAsProperty]get; } = null!;

        [Memento]
        [Reactive]
        public string Name { get; set; }

        public TileBase()
        {
            this.WhenAnyValue(x => x.ImagePath)
                .Select(x =>
                {
                    string path = Path.Combine(Environment.CurrentDirectory, "Images", "Tiles", x ?? "");
                    return File.Exists(path) ? path : string.Empty;
                })
                .ToPropertyEx(this, x => x.ImageSource);
        }
    }
}
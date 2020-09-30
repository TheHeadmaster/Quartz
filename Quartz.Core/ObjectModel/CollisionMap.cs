using System;
using System.Collections.Generic;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class CollisionMap : SaveableDatabaseObject
    {
        [Memento]
        [Reactive]
        public DirectionMap? CanEnterFrom { get; set; }

        [Memento]
        [Reactive]
        public DirectionMap? CanExitTo { get; set; }

        [Memento]
        [Reactive]
        public bool IsBush { get; set; }

        [Memento]
        [Reactive]
        public bool IsLadder { get; set; }
    }
}
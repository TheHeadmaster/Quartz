using System;
using System.Collections.Generic;
using System.Text;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    public class DirectionMap : SaveableDatabaseObject
    {
        [Memento]
        [Reactive]
        public bool East { get; set; }

        [Memento]
        [Reactive]
        public bool North { get; set; }

        [Memento]
        [Reactive]
        public bool South { get; set; }

        [Memento]
        [Reactive]
        public bool West { get; set; }
    }
}
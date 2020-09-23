using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ObjectModel
{
    public class PackSelection : ReactiveObject
    {
        [Reactive]
        public bool IsSelected { get; set; }

        [Reactive]
        public PackTemplate Pack { get; set; }
    }
}
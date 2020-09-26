using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    public class MainStatusBarViewModel : ReactiveObject
    {
        public Meta Metadata => App.Metadata;
    }
}
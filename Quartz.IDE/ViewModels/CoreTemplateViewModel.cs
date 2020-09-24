using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.ViewModels
{
    public class CoreTemplateViewModel : DocumentControlViewModel<CoreTemplate>
    {
        public override CoreTemplate Model { get; set; }
    }
}
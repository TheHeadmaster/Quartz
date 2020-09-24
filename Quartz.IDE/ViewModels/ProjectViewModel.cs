using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.ViewModels
{
    public class ProjectViewModel : DocumentControlViewModel<Project>
    {
        public override Project Model { get; set; }
    }
}
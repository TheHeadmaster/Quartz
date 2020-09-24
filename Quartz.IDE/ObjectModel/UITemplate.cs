using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ViewModels;

namespace Quartz.IDE.ObjectModel
{
    public class UITemplate : Template
    {
        public override DocumentControlViewModel CreateViewModel() => new UITemplateViewModel { Model = this };
    }
}
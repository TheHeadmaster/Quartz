﻿using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.ViewModels
{
    public class UITemplateViewModel : DocumentControlViewModel<UITemplate>
    {
        public override UITemplate Model { get; set; }
    }
}
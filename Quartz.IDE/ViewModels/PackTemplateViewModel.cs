﻿using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.ViewModels
{
    public class PackTemplateViewModel : DocumentControlViewModel<PackTemplate>
    {
        public override PackTemplate Model { get; set; }
    }
}
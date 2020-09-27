using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Quartz.IDE.ViewModels.Pages
{
    public class PageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CoreTemplatePage { get; set; }

        public DataTemplate ElementsPage { get; set; }

        public DataTemplate PackTemplatePage { get; set; }

        public DataTemplate ProjectPage { get; set; }

        public DataTemplate UITemplatePage { get; set; }

        public PageTemplateSelector() { }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) => item switch
        {
            ElementsViewModel _ => this.ElementsPage,
            ProjectViewModel _ => this.ProjectPage,
            CoreTemplateViewModel _ => this.CoreTemplatePage,
            UITemplateViewModel _ => this.UITemplatePage,
            PackTemplateViewModel _ => this.PackTemplatePage,
            _ => base.SelectTemplate(item, container),
        };
    }
}
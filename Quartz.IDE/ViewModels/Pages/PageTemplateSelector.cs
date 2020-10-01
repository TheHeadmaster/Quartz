using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Quartz.IDE.ViewModels.Pages
{
    /// <summary>
    /// Selects the <see cref="DataTemplate"/> for a page.
    /// </summary>
    public class PageTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// The <see cref="DataTemplate"/> for a <see cref="CoreTemplateViewModel"/>.
        /// </summary>
        public DataTemplate CoreTemplatePage { get; set; } = null!;

        /// <summary>
        /// The <see cref="DataTemplate"/> for an <see cref="ElementsViewModel"/>.
        /// </summary>
        public DataTemplate ElementsPage { get; set; } = null!;

        /// <summary>
        /// The <see cref="DataTemplate"/> for a <see cref="PackTemplateViewModel"/>.
        /// </summary>
        public DataTemplate PackTemplatePage { get; set; } = null!;

        /// <summary>
        /// The <see cref="DataTemplate"/> for a <see cref="ProjectViewModel"/>.
        /// </summary>
        public DataTemplate ProjectPage { get; set; } = null!;

        /// <summary>
        /// The <see cref="DataTemplate"/> for a <see cref="UITemplateViewModel"/>.
        /// </summary>
        public DataTemplate UITemplatePage { get; set; } = null!;

        /// <summary>
        /// The <see cref="DataTemplate"/> for a <see cref="TilesViewModel"/>.
        /// </summary>
        public DataTemplate TilesPage { get; set; } = null!;

        /// <summary>
        /// Creates a new <see cref="PageTemplateSelector"/>.
        /// </summary>
        public PageTemplateSelector() { }

        /// <summary>
        /// Returns a <see cref="DataTemplate"/> based on the type of the object passed in.
        /// </summary>
        /// <param name="item">
        /// The item to switch against.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// A <see cref="DataTemplate"/>.
        /// </returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => item switch
        {
            ElementsViewModel _ => this.ElementsPage,
            ProjectViewModel _ => this.ProjectPage,
            CoreTemplateViewModel _ => this.CoreTemplatePage,
            UITemplateViewModel _ => this.UITemplatePage,
            PackTemplateViewModel _ => this.PackTemplatePage,
            TilesViewModel _ => this.TilesPage,
            _ => base.SelectTemplate(item, container),
        };
    }
}
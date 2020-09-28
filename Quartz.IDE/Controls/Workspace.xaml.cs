#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvalonDock;
using AvalonDock.Layout;
using DynamicData;
using Quartz.IDE.ViewModels;
using Quartz.IDE.ViewModels.Pages;
using ReactiveUI;

namespace Quartz.IDE.Controls
{
    /// <summary>
    /// Interaction logic for Workspace.xaml
    /// </summary>
    public partial class Workspace : ReactiveUserControl<WorkspaceViewModel>
    {
        /// <summary>
        /// Creates a new <see cref="Workspace"/>.
        /// </summary>
        public Workspace()
        {
            this.InitializeComponent();

            this.ViewModel = new WorkspaceViewModel();

            this.WhenActivated(dispose =>
            {
                this.OneWayBind(this.ViewModel,
                    vm => vm.Pages,
                    view => view.MainDock.DocumentsSource)
                .DisposeWith(dispose);

                App.Metadata.WhenAnyValue(x => x.SelectedPage)
                .Subscribe(x => this.ChangeActivePage(x));
            });
        }

        private void DocumentClosing(object sender, DocumentClosingEventArgs args)
        {
            PageViewModel pageVM = (PageViewModel)args.Document.Content;

            App.Metadata.Pages.Remove(pageVM);
        }

        /// <summary>
        /// Changes the currently focused page to the page provided.
        /// </summary>
        /// <param name="page">
        /// The page to change focus to.
        /// </param>
        public void ChangeActivePage(PageViewModel page)
        {
            LayoutContent content = this.DocumentTabControl.Children.FirstOrDefault(x => x.Content == page);
            this._layoutRoot.ActiveContent = content;
        }
    }
}
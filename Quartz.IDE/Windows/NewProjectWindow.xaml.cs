using Librarium.WPF.Windows;
using Quartz.IDE.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quartz.IDE.Windows
{
    /// <summary>
    /// The <see cref="NewProjectWindow"/> allows the user to create a new project from a template.
    /// </summary>
    public partial class NewProjectWindow : BorderlessReactiveWindow<NewProjectWindowViewModel>
    {
        public NewProjectWindow()
        {
            this.InitializeComponent();

            this.ViewModel = new NewProjectWindowViewModel()
            {
                Parent = this,
                ProjectName = "My Project",
                ProjectPath = @"C:\"
            };

            this.ProjectNameTextBox.Watermark = "My Project";
            this.ProjectPathTextBox.Watermark = @"C:\";

            this.WhenActivated(dispose =>
            {
                this.OneWayBind(this.ViewModel,
                    vm => vm.CoreTemplates,
                    view => view.CoreTemplatesList.ItemsSource)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.UITemplates,
                    view => view.UITemplatesList.ItemsSource)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.PackTemplates,
                    view => view.PacksList.ItemsSource)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.PackTemplates,
                    view => view.PacksList.ItemsSource)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedCoreTemplate,
                    view => view.CoreTemplatesList.SelectedItem)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedUITemplate,
                    view => view.UITemplatesList.SelectedItem)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedCoreTemplate.Name,
                    view => view.CoreTemplateName.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedCoreTemplate.Author,
                    view => view.CoreTemplateAuthor.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedCoreTemplate.Version,
                    view => view.CoreTemplateVersion.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedCoreTemplate.Description,
                    view => view.CoreTemplateDescription.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedUITemplate.Name,
                    view => view.UITemplateName.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedUITemplate.Author,
                    view => view.UITemplateAuthor.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedUITemplate.Version,
                    view => view.UITemplateVersion.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedUITemplate.Description,
                    view => view.UITemplateDescription.Text)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.SelectedPackTemplate,
                    view => view.PacksList.SelectedItem)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedPackTemplate.Pack.Name,
                    view => view.PackTemplateName.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedPackTemplate.Pack.Author,
                    view => view.PackTemplateAuthor.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedPackTemplate.Pack.Version,
                    view => view.PackTemplateVersion.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.SelectedPackTemplate.Pack.Description,
                    view => view.PackTemplateDescription.Text)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.ProjectName,
                    view => view.ProjectNameTextBox.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.ProjectNameValidationText,
                    view => view.ProjectNameValidation.Text)
                .DisposeWith(dispose);

                this.Bind(this.ViewModel,
                    vm => vm.ProjectPath,
                    view => view.ProjectPathTextBox.Text)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.ProjectPathValidationText,
                    view => view.ProjectPathValidation.Text)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.CreateProject,
                    view => view.TemplateWizard,
                    nameof(this.TemplateWizard.Finish))
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                    vm => vm.BrowseFolder,
                    view => view.BrowseButton)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.IsCoreValid,
                    view => view.CorePage.CanSelectNextPage)
                .DisposeWith(dispose);

                this.OneWayBind(this.ViewModel,
                    vm => vm.IsUIValid,
                    view => view.UIPage.CanSelectNextPage)
                .DisposeWith(dispose);
            });
        }
    }
}
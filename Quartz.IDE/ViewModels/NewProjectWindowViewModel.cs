using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using DynamicData;
using DynamicData.Binding;
using Quartz.IDE.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels
{
    public class NewProjectWindowViewModel : ReactiveObject
    {
        private readonly ReadOnlyObservableCollection<CoreTemplate> coreTemplates;

        private readonly ReadOnlyObservableCollection<UITemplate> uiTemplates;

        public ReadOnlyObservableCollection<CoreTemplate> CoreTemplates => this.coreTemplates;

        [Reactive]
        public ObservableCollectionExtended<PackSelection> PackTemplates { get; set; }

        [Reactive]
        public Template SelectedCoreTemplate { get; set; }

        [Reactive]
        public PackSelection SelectedPackTemplate { get; set; }

        [Reactive]
        public Template SelectedUITemplate { get; set; }

        public SourceList<Template> Templates { get; } = new SourceList<Template>();

        public ReadOnlyObservableCollection<UITemplate> UITemplates => this.uiTemplates;

        public NewProjectWindowViewModel()
        {
            this.Templates.AddRange(TemplateManager.GetTemplates());
            this.Templates
                .Connect()
                .Filter(x => x is CoreTemplate)
                .Transform(x => (CoreTemplate)x)
                .Sort(SortExpressionComparer<CoreTemplate>.Ascending(x => x.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.coreTemplates)
                .Subscribe();

            this.Templates
                .Connect()
                .Filter(x => x is UITemplate)
                .Transform(x => (UITemplate)x)
                .Sort(SortExpressionComparer<UITemplate>.Ascending(x => x.Name))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.uiTemplates)
                .Subscribe();

            this.PackTemplates = new ObservableCollectionExtended<PackSelection>(this.Templates.Items
                .Where(x => x is PackTemplate)
                .Select(x => new PackSelection { Pack = (PackTemplate)x, IsSelected = false })
                .OrderBy(x => x.Pack.Name));
        }
    }
}
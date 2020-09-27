using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using DynamicData;
using DynamicData.Binding;
using Quartz.Core.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ViewModels.Pages
{
    public class ElementsViewModel : PageViewModel
    {
        private ReadOnlyObservableCollection<ElementMatchup> attackingMatchups;

        private ReadOnlyObservableCollection<ElementMatchup> defendingMatchups;

        private ReadOnlyObservableCollection<ElementMatchup> elementMatchups;

        private ReadOnlyObservableCollection<Element> elements;

        public ReadOnlyObservableCollection<ElementMatchup> AttackingMatchups => this.attackingMatchups;

        public ReadOnlyObservableCollection<ElementMatchup> DefendingMatchups => this.defendingMatchups;

        public ReadOnlyObservableCollection<ElementMatchup> ElementMatchups => this.elementMatchups;

        public ReadOnlyObservableCollection<Element> Elements => this.elements;

        [Reactive]
        public Element SelectedElement { get; set; }

        public ElementsViewModel()
        {
            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    x.SaveableObjects.Connect()
                    .Filter(x => x is Element)
                });

            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }
                    App.Metadata.CurrentProject.ElementMatchups.Connect()
                        .Sort(SortExpressionComparer<ElementMatchup>.Ascending(x => x.DefendingElement.Name))
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.elementMatchups)
                        .Subscribe();

                    App.Metadata.CurrentProject.ElementMatchups.Connect()
                        .Filter(x => x.AttackingElement == this.SelectedElement)
                        .Sort(SortExpressionComparer<ElementMatchup>.Ascending(x => x.AttackingElement.Name))
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.attackingMatchups)
                        .Subscribe();

                    App.Metadata.CurrentProject.ElementMatchups.Connect()
                        .Filter(x => x.DefendingElement == this.SelectedElement)
                        .Sort(SortExpressionComparer<ElementMatchup>.Ascending(x => x.DefendingElement.Name))
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.defendingMatchups)
                        .Subscribe();

                    App.Metadata.CurrentProject.Elements.Connect()
                        .Sort(SortExpressionComparer<Element>.Ascending(x => x.Name))
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.elements)
                        .Subscribe();
                });
        }
    }
}
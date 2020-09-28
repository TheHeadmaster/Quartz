using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using Quartz.Core.ObjectModel;
using Quartz.IDE.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Serilog;

namespace Quartz.IDE.ViewModels.Pages
{
    /// <summary>
    /// ViewModel for the <see cref="ElementsPage"/>.
    /// </summary>
    public sealed class ElementsViewModel : PageViewModel
    {
        private ReadOnlyObservableCollection<ElementMatchup>? attackingMatchups;

        private ReadOnlyObservableCollection<ElementMatchup>? defendingMatchups;

        private ReadOnlyObservableCollection<ElementMatchup>? elementMatchups;

        private ReadOnlyObservableCollection<Element>? elements;

        /// <summary>
        /// Gets whether all elements are saved.
        /// </summary>
        public bool AreElementsSaved { [ObservableAsProperty]get; } = true;

        /// <summary>
        /// Matchups in which the selected element is the attacking element.
        /// </summary>
        public ReadOnlyObservableCollection<ElementMatchup>? AttackingMatchups => this.attackingMatchups;

        /// <summary>
        /// Creates a new attacking matchup.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateAttackingMatchup { get; }

        /// <summary>
        /// Creates a new defending matchup.
        /// </summary>
        public ReactiveCommand<Unit, Unit> CreateDefendingMatchup { get; }

        /// <summary>
        /// Matchups in which the selected element is the defending element.
        /// </summary>
        public ReadOnlyObservableCollection<ElementMatchup>? DefendingMatchups => this.defendingMatchups;

        /// <summary>
        /// Represents the defensive capability rating of the selected element.
        /// </summary>
        [Reactive]
        public double DefensiveRating { get; private set; }

        /// <summary>
        /// A list of all element matchups.
        /// </summary>
        public ReadOnlyObservableCollection<ElementMatchup>? ElementMatchups => this.elementMatchups;

        /// <summary>
        /// A list of all elements.
        /// </summary>
        public ReadOnlyObservableCollection<Element>? Elements => this.elements;

        /// <summary>
        /// Creates a new element.
        /// </summary>
        public ReactiveCommand<Unit, Unit> NewElement { get; }

        /// <summary>
        /// Represents the offensive capability rating of the selected element.
        /// </summary>
        [Reactive]
        public double OffensiveRating { get; private set; }

        /// <summary>
        /// The currently selected element.
        /// </summary>
        [Reactive]
        public Element? SelectedElement { get; set; }

        /// <summary>
        /// Creates a new <see cref="ElementsViewModel"/>.
        /// </summary>
        public ElementsViewModel()
        {
            this.NewElement = ReactiveCommand.CreateFromTask(this.CreateElementAsync);

            this.CreateAttackingMatchup = ReactiveCommand.CreateFromTask(this.CreateAttackingMatchupAsync);

            this.CreateDefendingMatchup = ReactiveCommand.CreateFromTask(this.CreateDefendingMatchupAsync);

            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }
                    SaveableObject.SaveableObjects.Connect()
                    .WhenPropertyChanged(p => p.IsSaved)
                    .Select(p =>
                    {
                        return SaveableObject.SaveableObjects.Items.All(y =>
                        {
                            return !(y is Element || y is ElementMatchup) || y.IsSaved;
                        });
                    })
                    .ToPropertyEx(this, x => x.AreElementsSaved);
                });

            this.WhenAnyValue(x => x.AreElementsSaved)
                .Select(x =>
                {
                    return $"Elements{(x ? string.Empty : "*")}";
                })
                .ToPropertyEx(this, x => x.Header);

            App.Metadata.WhenAnyValue(x => x.CurrentProject)
                .Subscribe(x =>
                {
                    if (x is null) { return; }

                    // Connect to Elements
                    IObservable<ISortedChangeSet<Element, int>>? elements = App.Metadata.CurrentProject?.Elements.Connect()
                        .Sort(SortExpressionComparer<Element>.Ascending(x => x.Name));

                    // Elements Binding
                    elements.ObserveOn(RxApp.MainThreadScheduler).Bind(out this.elements).Subscribe();

                    // Connect to Element Matchups
                    IObservable<ISortedChangeSet<ElementMatchup, int>>? elementMatchups =
                    App.Metadata.CurrentProject?.ElementMatchups.Connect()
                        .AutoRefresh(x => x.Multiplier)
                        .AutoRefreshOnObservable(x => this.WhenAnyValue(x => x.SelectedElement))
                        .Sort(SortExpressionComparer<ElementMatchup>.Ascending(x => x.DefendingElement.Name));

                    // Element Matchups Binding
                    elementMatchups.ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.elementMatchups)
                        .Subscribe();

                    // Attacking Matchups Binding
                    elementMatchups.Filter(x =>
                        {
                            return x.AttackingElement == this.SelectedElement;
                        })
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.attackingMatchups)
                        .Subscribe();

                    // Defending Matchups Binding
                    elementMatchups.Filter(x =>
                        {
                            return x.DefendingElement == this.SelectedElement;
                        })
                        .ObserveOn(RxApp.MainThreadScheduler)
                        .Bind(out this.defendingMatchups)
                        .Subscribe();

                    // Offensive Rating Binding
                    elementMatchups.Select(x => this.GetOffensiveRating())
                        .BindTo(this, x => x.OffensiveRating);

                    elements.Select(x => this.GetOffensiveRating())
                        .BindTo(this, x => x.OffensiveRating);

                    // Defensive Rating Binding
                    elementMatchups.Select(x => this.GetDefensiveRating())
                        .BindTo(this, x => x.DefensiveRating);

                    elements.Select(x => this.GetDefensiveRating())
                        .BindTo(this, x => x.DefensiveRating);
                });
        }

        private Task CreateAttackingMatchupAsync()
        {
            Element element = this.Elements.Where(x => !this.ElementMatchups.Any(y => x == y.DefendingElement)).FirstOrDefault();

            if (element is { })
            {
                ElementMatchup matchup = new ElementMatchup
                {
                    AttackingElement = this.SelectedElement!,
                    DefendingElement = element,
                    Multiplier = 1.0,
                    ID = (this.ElementMatchups.DefaultIfEmpty().Max(x => x?.ID) ?? 0) + 1,
                    IsSaved = false,
                };
                App.Metadata.CurrentProject?.ElementMatchups.AddOrUpdate(matchup);
            }

            return Task.CompletedTask;
        }

        private Task CreateDefendingMatchupAsync()
        {
            Element element = this.Elements.Where(x => !this.ElementMatchups.Any(y => x == y.AttackingElement)).FirstOrDefault();

            if (element is { })
            {
                ElementMatchup matchup = new ElementMatchup
                {
                    DefendingElement = this.SelectedElement!,
                    AttackingElement = element,
                    Multiplier = 1.0,
                    ID = this.ElementMatchups.Max(x => x.ID) + 1,
                    IsSaved = false,
                };
                App.Metadata.CurrentProject?.ElementMatchups.AddOrUpdate(matchup);
            }

            return Task.CompletedTask;
        }

        private Task CreateElementAsync()
        {
            string newElementName = "New Element";
            int i = 2;
            while (this.Elements.Any(x => x.Name == newElementName))
            {
                newElementName = $"New Element {i}";
                i++;
            }
            Element element = new Element
            {
                Name = newElementName,
                ID = this.Elements.Max(x => x.ID) + 1,
                IsSaved = false
            };
            App.Metadata.CurrentProject?.Elements.AddOrUpdate(element);

            return Task.CompletedTask;
        }

        private double GetDefensiveRating()
        {
            List<ElementMatchup>? defendingMatchups = App.Metadata.CurrentProject?.ElementMatchups.Items
                .Where(x => x.DefendingElement == this.SelectedElement).ToList();
            if (defendingMatchups is null) { return 0; }

            double sum = defendingMatchups.Sum(x => x.Multiplier);
            sum += App.Metadata.CurrentProject!.Elements.Count - defendingMatchups.Count;

            double rating = sum / App.Metadata.CurrentProject.Elements.Count * 5;
            return double.IsNaN(rating) ? 0 : 10 - Math.Round(rating, 2, MidpointRounding.AwayFromZero);
        }

        private double GetOffensiveRating()
        {
            List<ElementMatchup>? attackingMatchups = App.Metadata.CurrentProject?.ElementMatchups.Items
                .Where(x => x.AttackingElement == this.SelectedElement).ToList();
            if (attackingMatchups is null) { return 0; }

            double sum = attackingMatchups.Sum(x => x.Multiplier);
            sum += App.Metadata.CurrentProject!.Elements.Count - attackingMatchups.Count;

            double rating = sum / App.Metadata.CurrentProject.Elements.Count * 5;
            return double.IsNaN(rating) ? 0 : Math.Round(rating, 2, MidpointRounding.AwayFromZero);
        }
    }
}
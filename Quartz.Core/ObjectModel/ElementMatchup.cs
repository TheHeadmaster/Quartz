using System;
using System.Collections.Generic;
using System.Text;
using PersistentEntity;
using Quartz.Core.ObjectModel.Attributes;
using ReactiveUI.Fody.Helpers;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// Represents the efficacy of one element against another.
    /// </summary>
    public class ElementMatchup : SaveableDatabaseObject
    {
        /// <summary>
        /// The attacking element.
        /// </summary>
        [Memento]
        [Reactive]
        public Element AttackingElement { get; set; } = null!;

        /// <summary>
        /// The ID of the attacking element.
        /// </summary>
        [Memento]
        [Reactive]
        public int AttackingElementID { get; set; }

        /// <summary>
        /// The defending element.
        /// </summary>
        [Memento]
        [Reactive]
        public Element DefendingElement { get; set; } = null!;

        /// <summary>
        /// The ID of the defending element.
        /// </summary>
        [Memento]
        [Reactive]
        public int DefendingElementID { get; set; }

        /// <summary>
        /// Represents the damage multiplier for the matchup. 0.0 means immune, and 1.0 is normal damage.
        /// </summary>
        [Memento]
        [Reactive]
        public double Multiplier { get; set; }
    }
}
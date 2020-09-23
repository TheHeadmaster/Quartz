using System;
using System.Collections.Generic;
using System.Text;
using PersistentEntity;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// Represents the efficacy of one element against another.
    /// </summary>
    public class ElementMatchup : DatabaseObject
    {
        /// <summary>
        /// The attacking element.
        /// </summary>
        public Element AttackingElement { get; set; }

        /// <summary>
        /// The ID of the attacking element.
        /// </summary>
        public int AttackingElementID { get; set; }

        /// <summary>
        /// The defending element.
        /// </summary>
        public Element DefendingElement { get; set; }

        /// <summary>
        /// The ID of the defending element.
        /// </summary>
        public int DefendingElementID { get; set; }

        /// <summary>
        /// Represents the damage multiplier for the matchup. 0.0 means immune, and 1.0 is normal damage.
        /// </summary>
        public double Multiplier { get; set; }
    }
}
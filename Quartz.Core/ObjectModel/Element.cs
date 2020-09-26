using System;
using System.Collections.Generic;
using System.Text;
using PersistentEntity;
using Quartz.Core.ObjectModel.Attributes;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// Represents an elemental type.
    /// </summary>
    public class Element : SaveableDatabaseObject
    {
        /// <summary>
        /// The list of damage matchups defined for this element.
        /// </summary>
        [Memento]
        public List<ElementMatchup> ElementMatchups { get; set; }

        /// <summary>
        /// The name of the element.
        /// </summary>
        [Memento]
        public string Name { get; set; }
    }
}
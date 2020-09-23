using System;
using System.Collections.Generic;
using System.Text;
using PersistentEntity;

namespace Quartz.Core.ObjectModel
{
    /// <summary>
    /// Represents an elemental type.
    /// </summary>
    public class Element : DatabaseObject
    {
        /// <summary>
        /// The list of damage matchups defined for this element.
        /// </summary>
        public List<ElementMatchup> ElementMatchups { get; set; }

        /// <summary>
        /// The name of the element.
        /// </summary>
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a <see cref="PackTemplate"/> and whether or not its selected to be included in
    /// the new project.
    /// </summary>
    public class PackSelection : ReactiveObject
    {
        /// <summary>
        /// Gets or sets whether the <see cref="PackTemplate"/> is selected.
        /// </summary>
        [Reactive]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets the <see cref="PackTemplate"/>.
        /// </summary>
        public PackTemplate Pack { get; }

        /// <summary>
        /// Creates a new <see cref="PackSelection"/> with a <see cref="PackTemplate"/> and an
        /// initial selection value.
        /// </summary>
        /// <param name="pack">
        /// The <see cref="PackTemplate"/>.
        /// </param>
        /// <param name="isSelected">
        /// Sets whether the <see cref="PackTemplate"/> is initially selected or not.
        /// </param>
        public PackSelection(PackTemplate pack, bool isSelected)
        {
            this.Pack = pack;
            this.IsSelected = isSelected;
        }
    }
}
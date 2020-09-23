using System;
using System.Collections.Generic;
using System.Text;
using Quartz.IDE.ViewModels;

namespace Quartz.IDE.ObjectModel
{
    public abstract class DocumentControl : ObservableObject
    {
        /// <summary>
        /// What should be displayed in menus and document tabs containing this <see cref="DocumentControl"/>.
        /// </summary>
        public abstract string Header { get; }

        /// <summary>
        /// Creates a <see cref="DocumentControlViewModel"/> containing this <see cref="DocumentControl"/>.
        /// </summary>
        public abstract DocumentControlViewModel CreateViewModel();

        /// <summary>
        /// Deletes the object.
        /// </summary>
        public abstract void Delete();

        /// <summary>
        /// Saves this <see cref="DocumentControl"/> to disk.
        /// </summary>
        public abstract void Save();
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Librarium.Json;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz project.
    /// </summary>
    public class Project : JFileDocumentControl
    {
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of Quartz that the project was made with.
        /// </summary>
        public Version Version { get; set; }

        public void Load()
        {
        }

        public void Save() => throw new NotImplementedException();
    }
}
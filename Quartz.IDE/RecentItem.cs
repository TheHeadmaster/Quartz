using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.IDE
{
    /// <summary>
    /// Represents a recently loaded project.
    /// </summary>
    public class RecentItem
    {
        /// <summary>
        /// The display header for menu items.
        /// </summary>
        public string Header => $"\"{this.Name}\" {this.Path}";

        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The path to the project file.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The date the project was last accessed.
        /// </summary>
        public DateTime Timestamp { get; }

        public RecentItem(string name, string path, DateTime timestamp)
        {
            this.Name = name;
            this.Path = path;
            this.Timestamp = timestamp;
        }
    }
}
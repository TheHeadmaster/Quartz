using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Librarium.Json;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz template.
    /// </summary>
    public abstract class Template : IModelToFile
    {
        private static List<Type> subclasses;

        public static List<Type> Subclasses
        {
            get
            {
                if (subclasses is null || subclasses.Count == 0)
                {
                    List<Type> newSubClasses = new List<Type>();
                    foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        newSubClasses.AddRange(asm.GetTypes().Where(x => x.IsSubclassOf(typeof(Template))));
                    }
                    subclasses = newSubClasses;
                }
                return subclasses;
            }
        }

        /// <summary>
        /// The template author's name or alias.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The description of the template.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The filename of the teplate file on disk.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The path to the template file on disk.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The version of Quartz that the template was made with.
        /// </summary>
        public Version Version { get; set; }

        public void Load()
        {
        }

        public void Save() => throw new NotImplementedException();
    }
}
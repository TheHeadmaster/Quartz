using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Librarium.Json;
using Quartz.IDE.Json;
using Quartz.IDE.ViewModels;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz template.
    /// </summary>
    public abstract class Template : JFileDocumentControl
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

        public override string Header => this.Name;

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
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            this.IsSaved = true;
        }

        /// <summary>
        /// Creates a new json data model, populates it with this project's data, and saves it to disk.
        /// </summary>
        public override void Save()
        {
            TemplateFile file = new TemplateFile();
            file.PopulateFile(this);
            file.Save();
            this.IsSaved = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Librarium.Json;
using Quartz.Core.ObjectModel;
using Quartz.IDE.Json;
using Quartz.IDE.ViewModels;
using PersistentEntity;
using System.Threading.Tasks;

namespace Quartz.IDE.ObjectModel
{
    /// <summary>
    /// Represents a Quartz template.
    /// </summary>
    public abstract class Template : SaveableObject, IModelToFile
    {
        private static List<Type>? subclasses;

        /// <summary>
        /// Gets a list of all classes that derive from <see cref="Template"/>.
        /// </summary>
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
        public string? Author { get; set; }

        /// <summary>
        /// The description of the template.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The name of the <see cref="JFile"/> on disk.
        /// </summary>
        public string FileName { get; set; } = "";

        /// <summary>
        /// The path to the <see cref="JFile"/> on disk.
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// The name of the template.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The version of Quartz that the template was made with.
        /// </summary>
        public Version? Version { get; set; }

        public void Load()
        {
            Directory.CreateDirectory(Path.Combine(this.FilePath, "Images"));
            this.IsSaved = true;
        }

        /// <summary>
        /// Saves the <see cref="JFile"/> to disk.
        /// </summary>
        public void Save()
        {
            TemplateFile file = new TemplateFile();
            file.PopulateFile(this);
            file.Save();
        }

        /// <summary>
        /// Creates a new json data model, populates it with this project's data, and saves it to disk.
        /// </summary>
        public override Task SaveAsync(Connection? connection = null)
        {
            this.Save();
            this.IsSaved = true;
            return Task.CompletedTask;
        }
    }
}
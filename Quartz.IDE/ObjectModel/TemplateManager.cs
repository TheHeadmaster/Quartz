using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Librarium.Core;
using Librarium.Json;
using Quartz.IDE.Json;

namespace Quartz.IDE.ObjectModel
{
    public static class TemplateManager
    {
        /// <summary>
        /// A basic blank template.
        /// </summary>
        private static CoreTemplate BlankTemplate => new CoreTemplate
        {
            Name = "Blank Template",
            Author = "Quartz",
            Version = App.Metadata.Version,
            Description = "A completely blank slate to create a from-scratch project. Recommended for advanced users only."
        };

        /// <summary>
        /// The directory where the user's templates are stored.
        /// </summary>
        public static string TemplateDirectory => Path.Combine(App.Metadata.AppDataDirectory, "Templates");

        /// <summary>
        /// Gets all templates defined in the user's AppData directory.
        /// </summary>
        /// <returns>
        /// </returns>
        public static List<Template> GetTemplates()
        {
            Directory.CreateDirectory(TemplateDirectory);
            List<Template> templates = new List<Template> { BlankTemplate };
            foreach (string directory in Directory.GetDirectories(TemplateDirectory))
            {
                string templateFile = Path.Combine(directory, "template.json");
                if (!File.Exists(templateFile)) { continue; }
                templates.Add(JFile.Load<TemplateFile>(templateFile).CreateModel());
            }
            return templates;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Librarium.Core;
using Librarium.Json;
using Quartz.Core.Diagnostics;
using Quartz.IDE.Json;
using Serilog;

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
        [Log("Getting templates from disk...")]
        public static List<Template> GetTemplates()
        {
            Directory.CreateDirectory(TemplateDirectory);
            List<Template> templates = new List<Template> { BlankTemplate };
            foreach (string directory in Directory.GetDirectories(TemplateDirectory))
            {
                Log.Information("Template found: {Directory}. Loading...", Path.GetDirectoryName(directory));
                string templateFile = Path.Combine(directory, "Template.json");
                if (!File.Exists(templateFile))
                {
                    Log.Warning("{Directory} Template could not be loaded. Template.json file is invalid or missing.", Path.GetDirectoryName(directory));
                    continue;
                }
                templates.Add(JFile.Load<TemplateFile>(templateFile).CreateModel());
                Log.Information("Load template {Directory} was successful.", Path.GetDirectoryName(directory));
            }
            Log.Information("Total templates loaded: {Templates}", templates.Count);
            return templates;
        }
    }
}
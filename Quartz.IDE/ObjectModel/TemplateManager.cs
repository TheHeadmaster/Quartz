using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Description = "A completely blank slate to create a from-scratch project. Recommended for advanced users only.",
            FileName = "Template.json",
            FilePath = Path.Combine(AppMeta.AssemblyDirectory, "Blank Template")
        };

        private static UITemplate DefaultUITemplate => new UITemplate
        {
            Name = "Default UI Template",
            Author = "Quartz",
            Version = App.Metadata.Version,
            Description = "The default User Interface."
        };

        /// <summary>
        /// The directory where the user's templates are stored.
        /// </summary>
        public static string TemplateDirectory => Path.Combine(App.Metadata.AppDataDirectory, "Templates");

        [Log("Creating project from template...", "Project creation completed.", "Project creation failed.")]
        public static Project CreateFromTemplates(CoreTemplate core, UITemplate ui, List<PackTemplate> packs, string filePath, string name)
        {
            Project project = new Project
            {
                Name = name,
                FileName = "Project.json",
                FilePath = filePath,
                Version = AppMeta.CurrentVersion
            };

            List<Template> templates = new List<Template>
            {
                core,
                ui
            };

            templates.AddRange(packs);

            foreach (Template template in templates.Where(x => !x.FilePath.IsNullOrWhiteSpace()))
            {
                Log.Information("Copying files for template {TemplateName}.", template.Name);
                foreach (string dirPath in Directory.GetDirectories(template.FilePath, "*", SearchOption.AllDirectories))
                {
                    DirectoryInfo info = Directory.CreateDirectory(dirPath.Replace(template.FilePath, filePath));
                    Log.Information("Creating Directory: {Directory}", info.Name);
                }

                foreach (string newPath in Directory.GetFiles(template.FilePath, "*.*", SearchOption.AllDirectories))
                {
                    FileInfo file = new FileInfo(newPath);
                    if (file.Name == "Template.json") { continue; }
                    Log.Information("Copying File: {File} to {Directory}.", file.Name, file.DirectoryName);
                    File.Copy(newPath, newPath.Replace(template.FilePath, filePath), true);
                }
            }

            return project;
        }

        /// <summary>
        /// Gets all templates defined in the user's AppData directory.
        /// </summary>
        [Log("Getting templates from disk...")]
        public static List<Template> GetTemplates()
        {
            Directory.CreateDirectory(TemplateDirectory);
            List<Template> templates = new List<Template> { BlankTemplate, DefaultUITemplate };
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
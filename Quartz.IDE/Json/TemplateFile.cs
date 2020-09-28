using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Librarium.Json;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.Json
{
    /// <summary>
    /// Represents a Quartz template file on disk.
    /// </summary>
    public class TemplateFile : JFile, IFileToModel<Template>
    {
        /// <summary>
        /// The template author's name or alias.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// The description of the template.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The type of the Quartz template.
        /// </summary>
        public string? TemplateType { get; set; }

        /// <summary>
        /// The version of Quartz that the template was made with.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Creates a new <see cref="Template"/> model.
        /// </summary>
        /// <returns>
        /// </returns>
        public Template CreateModel()
        {
            Template? newTemplate = (Template?)Activator.CreateInstance(
                Template.Subclasses.FirstOrDefault(x => x.Name.Contains(this.TemplateType!)));

            if (newTemplate is null) { throw new ArgumentException($"TemplateType {this.TemplateType} is not a valid subclass of Template."); }

            System.Version.TryParse(this.Version, out Version? version);

            newTemplate.Name = this.Name;
            newTemplate.FileName = this.FileName;
            newTemplate.FilePath = this.FilePath;
            newTemplate.Version = version;
            newTemplate.Author = this.Author;
            newTemplate.Description = this.Description;

            newTemplate.IsSaved = true;
            return newTemplate;
        }

        /// <summary>
        /// Populates the <see cref="JFile"/> with the specified model's values.
        /// </summary>
        /// <param name="model">
        /// The module to populate the <see cref="JFile"/> with.
        /// </param>
        public void PopulateFile(Template model)
        {
            this.Name = model.Name;
            this.FileName = model.FileName;
            this.FilePath = model.FilePath;
            this.Version = model.Version?.ToString();
            this.Author = model.Author;
            this.Description = model.Description;
            this.TemplateType = model.GetType().Name;
        }
    }
}
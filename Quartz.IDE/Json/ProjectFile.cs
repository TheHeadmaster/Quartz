using System;
using System.Collections.Generic;
using System.Text;
using Librarium.Json;
using Quartz.IDE.ObjectModel;

namespace Quartz.IDE.Json
{
    /// <summary>
    /// Represents a Quartz project file on disk.
    /// </summary>
    public class ProjectFile : JFile, IFileToModel<Project>
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public Project CreateModel() => throw new NotImplementedException();

        public void PopulateFile(Project model) => throw new NotImplementedException();

        //public Project CreateModel()
        //{
        //    //System.Version.TryParse(this.Version, out Version version);
        //    //Project project = new Project()
        //    //{
        //    //    Name = this.Name,
        //    //    FileName = this.FileName,
        //    //    FilePath = this.FilePath,
        //    //    Version = version,
        //    //};
        //    //project.IsSaved = true;
        //    //return project;
        //}
        //
        //public void PopulateFile(Project model)
        //{
        //    //this.Name = model.Name;
        //    //this.FileName = model.FileName;
        //    //this.FilePath = model.FilePath;
        //    //this.Version = model.Version?.ToString() ?? null;
        //}
    }
}
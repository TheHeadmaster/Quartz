using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Librarium.Core;
using Librarium.Json;

namespace Quartz.IDE.ObjectModel
{
    public abstract class JFileDocumentControl : DocumentControl, IModelToFile
    {
        /// <summary>
        /// The filename of the project file on disk.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The path to the project file on disk.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The full path, including the filename and extension, to the project file on disk.
        /// </summary>
        public string FullPath => Path.Combine(this.FilePath, this.FileName);

        /// <summary>
        /// Deletes the file.
        /// </summary>
        public override void Delete()
        {
            if (this.FilePath.IsNullOrWhiteSpace() || this.FileName.IsNullOrWhiteSpace()) { return; }
            if (File.Exists(this.FullPath))
            {
                File.Delete(this.FullPath);
            }
        }
    }
}
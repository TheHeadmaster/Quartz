using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Librarium.Core;
using ReactiveUI;

namespace Quartz.IDE.ViewModels
{
    /// <summary>
    /// View Model for the <see cref="AboutWindow"/>.
    /// </summary>
    public class AboutWindowViewModel : ReactiveObject
    {
        public string Changelog
        {
            get
            {
                using (Stream stream = Application.GetResourceStream(new Uri($"/Quartz.IDE;component/Resources/Documents/CHANGELOG.md", UriKind.Relative)).Stream)
                {
                    if (stream is null)
                    {
                        return "Could not find CHANGELOG.md";
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string s = reader.ReadToEnd();
                        s = s.Replace("&#x27;", "'");
                        Regex regex = new Regex("<a name=\".*\"><\\/a>[\\s]");
                        return regex.Replace(s, string.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the version of the executing assembly as a string.
        /// </summary>
        public string Version => $"Version {AppMeta.CurrentVersion.Major}.{AppMeta.CurrentVersion.Minor}.{AppMeta.CurrentVersion.Build} Revision {AppMeta.CurrentVersion.Revision}";
    }
}
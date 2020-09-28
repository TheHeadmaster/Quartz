using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using Librarium.Core;

namespace Quartz.IDE.ViewModels.Validation
{
    public class NewProjectWindowValidation : AbstractValidator<NewProjectWindowViewModel>
    {
        public NewProjectWindowValidation()
        {
            this.RuleFor(x => x.ProjectName).Must(x => this.IsValidName(x))
                .WithMessage("Project name can only contain the characters [a-zA-Z0-9-_'] and whitespace, and must not be null.");
            this.RuleFor(x => x.ProjectPath).Must(x => this.IsValidPath(x)).WithMessage("Project path must be a valid absolute path.");
        }

        private bool IsValidName(string name)
        {
            if (name.IsNullOrWhiteSpace()) { return false; }
            Regex reg = new Regex(@"^[\w\d\s-\']+$");
            return reg.IsMatch(name);
        }

        private bool IsValidPath(string path)
        {
            if (path.IsNullOrWhiteSpace()) { return false; }
            try
            {
                if (!Path.IsPathRooted(path)) { return false; }
                Path.GetFullPath(path);
            }
            catch
            {
                return false;
            }

            char[] invalidPathChars = Path.GetInvalidPathChars();

            return !path.Any(x => invalidPathChars.Contains(x));
        }
    }
}
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Librarium.Commands;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Locator;
using Microsoft.Build.Logging;
using Microsoft.CSharp;
using Quartz.Core.Diagnostics;
using Serilog;

namespace Quartz.IDE.Commands
{
    /// <summary>
    /// Compiles the project from source and runs a debugger-attached instance of the compiled game.
    /// </summary>
    public class CompileAndRunCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CompileAndRunCommand();

        public static BuildResult RunMsBuild(string projectFile, string target, IDictionary<string, string> properties)
        {
            BuildParameters parameters = new BuildParameters(new ProjectCollection())
            {
                Loggers = new Microsoft.Build.Framework.ILogger[] { new BuildLogger() }
            };
            return BuildManager.DefaultBuildManager.Build(
                parameters,
                new BuildRequestData(projectFile, properties, null, new[] { target }, null));
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            string projectDirectory = @"C:\users\david\Dropbox\Development\Visual Studio\Quartz\Quartz.Engine";
            IDictionary<string, string> properties = new Dictionary<string, string>
            {
                { "Configuration", "Release" }
            };
            BuildResult result = RunMsBuild($@"{projectDirectory}\Quartz.Engine.csproj", "Build", properties);
        }

        public class BuildLogger : Microsoft.Build.Framework.ILogger
        {
            public string Parameters { get; set; }

            public LoggerVerbosity Verbosity { get; set; }

            private void EventSource_ErrorRaised(object sender, BuildErrorEventArgs e)
            {
                Log.Error(e.Message);
            }

            private void EventSource_MessageRaised(object sender, BuildMessageEventArgs e)
            {
                Log.Information(e.Message);
            }

            public void Initialize(IEventSource eventSource)
            {
                eventSource.MessageRaised += this.EventSource_MessageRaised;
                eventSource.ErrorRaised += this.EventSource_ErrorRaised;
            }

            public void Shutdown() { }
        }
    }
}
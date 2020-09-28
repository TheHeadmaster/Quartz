using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Quartz.IDE.UI;

namespace Quartz.IDE.Engine
{
    public static class GameCompiler
    {
        public static void Build()
        {
            App.Metadata.StatusBarMessage = "Building...";
            App.Metadata.IsRunning = true;
            App.Metadata.StatusBarColor = StatusBarColor.Warning;
            string projectDirectory = @"C:\users\david\Dropbox\Development\Visual Studio\Quartz\Quartz.Engine";
            IDictionary<string, string> properties = new Dictionary<string, string>
            {
                { "Configuration", "Release" }
            };
            BuildResult result = RunMsBuild($@"{projectDirectory}\Quartz.Engine.csproj", "Build", properties);
        }

        public static BuildResult RunMsBuild(string projectFile, string target, IDictionary<string, string> properties)
        {
            BuildParameters parameters = new BuildParameters(new ProjectCollection())
            {
                Loggers = new Microsoft.Build.Framework.ILogger[] { new GameBuildLogger() }
            };
            return BuildManager.DefaultBuildManager.Build(
                parameters,
                new BuildRequestData(projectFile, properties, null, new[] { target }, null));
        }
    }
}
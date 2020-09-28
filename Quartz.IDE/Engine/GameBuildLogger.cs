using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using Serilog;

namespace Quartz.IDE.Engine
{
    public class GameBuildLogger : Microsoft.Build.Framework.ILogger
    {
        public string Parameters { get; set; } = null!;

        public LoggerVerbosity Verbosity { get; set; } = default;

        private void EventSource_ErrorRaised(object sender, BuildErrorEventArgs args) => Log.Error("{Message}", args.Message);

        private void EventSource_MessageRaised(object sender, BuildMessageEventArgs args) => Log.Information("{Message}", args.Message);

        public void Initialize(IEventSource eventSource)
        {
            eventSource.MessageRaised += this.EventSource_MessageRaised;
            eventSource.ErrorRaised += this.EventSource_ErrorRaised;
        }

        public void Shutdown() { }
    }
}
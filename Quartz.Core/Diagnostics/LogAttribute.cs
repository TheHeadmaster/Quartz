using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Librarium.Core;
using MethodBoundaryAspect.Fody.Attributes;
using Serilog;

namespace Quartz.Core.Diagnostics
{
    /// <summary>
    /// Logs a message to the sink pool before and/or after a method call.
    /// </summary>
    public sealed class LogAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        /// The message to log before the method is called.
        /// </summary>
        public string EntryMessage { get; private set; }

        /// <summary>
        /// The message to log if the method throws an exception.
        /// </summary>
        public string ExceptionMessage { get; private set; }

        /// <summary>
        /// The message to log after a method is called.
        /// </summary>
        public string ExitMessage { get; private set; }

        /// <summary>
        /// Determines whether the thrown exception should be considered a fatal exception.
        /// </summary>
        public bool IsExceptionFatal { get; private set; }

        /// <summary>
        /// Logs a message to the sink pool before and/or after a method call.
        /// </summary>
        /// <param name="entryMessage">
        /// The message to log before the method is called.
        /// </param>
        /// <param name="exitMessage">
        /// The message to log after a method is completed.
        /// </param>
        /// <param name="exceptionMessage">
        /// The message to log in the event that the method throws an exception.
        /// </param>
        /// <param name="IsExceptionFatal">
        /// Tells the logger that any exception thrown in this method is considered a fatal
        /// exception, and should be logged as such.
        /// </param>
        public LogAttribute(string entryMessage = "", string exitMessage = "", string exceptionMessage = "", bool IsExceptionFatal = false)
        {
            this.EntryMessage = entryMessage;
            this.ExitMessage = exitMessage;
            this.ExceptionMessage = exceptionMessage;
            this.IsExceptionFatal = IsExceptionFatal;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!this.EntryMessage.IsNullOrWhiteSpace())
            {
                Log.Information("{EntryMessage}", this.EntryMessage);
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            if (this.IsExceptionFatal)
            {
                if (!this.ExceptionMessage.IsNullOrWhiteSpace())
                {
                    Log.Fatal(args.Exception, "");
                }
                else
                {
                    Log.Fatal(args.Exception, "{Exception}", this.ExceptionMessage);
                }
            }
            else
            {
                if (!this.ExceptionMessage.IsNullOrWhiteSpace())
                {
                    Log.Error(args.Exception, "");
                }
                else
                {
                    Log.Error(args.Exception, "{Exception}", this.ExceptionMessage);
                }
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.ReturnValue is Task t)
            {
                t.ContinueWith(task =>
                {
                    if (!this.ExitMessage.IsNullOrWhiteSpace())
                    {
                        Log.Information("{ExitMessage}", this.ExitMessage);
                    }
                });
            }
            else
            {
                if (!this.ExitMessage.IsNullOrWhiteSpace())
                {
                    Log.Information("{ExitMessage}", this.ExitMessage);
                }
            }
        }
    }
}
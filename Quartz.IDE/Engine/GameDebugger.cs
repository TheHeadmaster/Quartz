using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Quartz.IDE.UI;

namespace Quartz.IDE.Engine
{
    public static class GameDebugger
    {
        public static void Run()
        {
            App.Metadata.StatusBarMessage = "Running...";
            App.Metadata.IsRunning = true;
            App.Metadata.StatusBarColor = StatusBarColor.Debugging;

            new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = @"C:\Users\david\Dropbox\Development\Visual Studio\Quartz\Quartz.Engine\bin\Debug\netcoreapp3.1\Quartz.Engine.exe",
                }
            }.Start();
        }
    }
}
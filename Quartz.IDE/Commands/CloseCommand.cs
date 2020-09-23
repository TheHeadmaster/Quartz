using Librarium.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Quartz.IDE.Commands
{
    /// <summary>
    /// Closes the application.
    /// </summary>
    public class CloseCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new CloseCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
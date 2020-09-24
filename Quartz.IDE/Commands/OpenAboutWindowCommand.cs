using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Librarium.Commands;
using Quartz.IDE.Windows;

namespace Quartz.IDE.Commands
{
    public class OpenAboutWindowCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenAboutWindowCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            AboutWindow wnd = new AboutWindow();
            wnd.ShowDialog();
        }
    }
}
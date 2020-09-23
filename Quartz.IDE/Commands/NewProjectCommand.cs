using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Librarium.Commands;
using Quartz.IDE.Windows;

namespace Quartz.IDE.Commands
{
    /// <summary>
    /// Creates a new project.
    /// </summary>
    public class NewProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new NewProjectCommand();

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if (App.Metadata.CurrentProject is null)
            {
                NewProjectWindow wnd = new NewProjectWindow();
                wnd.ShowDialog();
            }
            else
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {App.Metadata.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    App.Metadata.CurrentProject.Save();
                    NewProjectWindow wnd = new NewProjectWindow();
                    wnd.ShowDialog();
                }
                else if (result == MessageBoxResult.No)
                {
                    NewProjectWindow wnd = new NewProjectWindow();
                    wnd.ShowDialog();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
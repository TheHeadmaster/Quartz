using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace Quartz.IDE.UI
{
    /// <summary>
    /// Extension methods for Reactive.
    /// </summary>
    public static class ReactiveExtensions
    {
        /// <summary>
        /// Binds a Command to an Input Binding.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The ViewModel that the command is in.
        /// </typeparam>
        /// <typeparam name="TView">
        /// The view that the input binding is in.
        /// </typeparam>
        /// <typeparam name="TVMCmd">
        /// The Command.
        /// </typeparam>
        /// <param name="view">
        /// The view that the input binding is in.
        /// </param>
        /// <param name="viewModel">
        /// The ViewModel that the command is in.
        /// </param>
        /// <param name="vmCmd">
        /// The command that fires when the hotkey is pressed.
        /// </param>
        /// <param name="gesture">
        /// The hotkey input gesture that activates the command.
        /// </param>
        public static void BindHotkey<TViewModel, TView, TVMCmd>(this TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMCmd>> vmCmd, InputGesture gesture)
            where TViewModel : class
            where TView : UIElement, IViewFor
            where TVMCmd : class, ICommand
        {
            TVMCmd command = vmCmd.Compile().Invoke(viewModel);

            switch (gesture)
            {
                case KeyGesture key:
                    {
                        KeyBinding binding = new KeyBinding(command, key);
                        view.InputBindings.Add(binding);
                        break;
                    }
                case MouseGesture mouse:
                    {
                        MouseBinding binding = new MouseBinding(command, mouse);
                        view.InputBindings.Add(binding);
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }
    }
}
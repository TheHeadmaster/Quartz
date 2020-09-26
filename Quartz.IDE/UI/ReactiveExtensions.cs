using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace Quartz.IDE.UI
{
    public static class ReactiveExtensions
    {
        public static void BindHotkey<TViewModel, TView, TVMCmd>(this TView view, TViewModel viewModel, Expression<Func<TViewModel, TVMCmd>> vmProperty, InputGesture gesture)
            where TViewModel : class
            where TView : UIElement, IViewFor
            where TVMCmd : class, ICommand
        {
            TVMCmd command = vmProperty.Compile().Invoke(viewModel);

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
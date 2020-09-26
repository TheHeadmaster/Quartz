using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Quartz.IDE.UI
{
    public class StatusBarColor
    {
        public static StatusBarColor Debugging { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA00AA"))
        };

        public static StatusBarColor Error { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA0000"))
        };

        public static StatusBarColor Idle { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007ACC"))
        };

        public static StatusBarColor Processing { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFB6")),
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"))
        };

        public static StatusBarColor Warning { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAA00"))
        };

        public SolidColorBrush Color { get; private set; }

        public SolidColorBrush Foreground { get; private set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

        private StatusBarColor() { }
    }
}
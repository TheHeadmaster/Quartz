using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Quartz.IDE.UI
{
    /// <summary>
    /// Represents the theming for the status bar.
    /// </summary>
    public class StatusBarColor
    {
        /// <summary>
        /// Used for when a test is running with an attached debugging process to it.
        /// </summary>
        public static StatusBarColor Debugging { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA00AA"))
        };

        /// <summary>
        /// Used for whenever something throws an error.
        /// </summary>
        public static StatusBarColor Error { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA0000"))
        };

        /// <summary>
        /// Used for whenever the program is idle or ready.
        /// </summary>
        public static StatusBarColor Idle { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007ACC"))
        };

        /// <summary>
        /// Used for whenever something is currently being processed.
        /// </summary>
        public static StatusBarColor Processing { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFB6")),
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"))
        };

        /// <summary>
        /// Used for whenever something throws a warning.
        /// </summary>
        public static StatusBarColor Warning { get; } = new StatusBarColor()
        {
            Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAA00"))
        };

        /// <summary>
        /// The background color for the status bar.
        /// </summary>
        public SolidColorBrush Color { get; private set; } = null!;

        /// <summary>
        /// The font color for the status bar.
        /// </summary>
        public SolidColorBrush Foreground { get; private set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

        private StatusBarColor() { }
    }
}
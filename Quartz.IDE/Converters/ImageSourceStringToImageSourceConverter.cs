using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Librarium.Core;

namespace Quartz.IDE.Converters
{
    /// <summary>
    /// Converts string path to Image Source and back.
    /// </summary>
    public class ImageSourceStringToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts string path to Image Source.
        /// </summary>
        /// <param name="value">
        /// The string path.
        /// </param>
        /// <param name="targetType">
        /// </param>
        /// <param name="parameter">
        /// </param>
        /// <param name="culture">
        /// </param>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null) { return null; }
            string path = (string)value;
            if (path.IsNullOrWhiteSpace()) { return null; }
            BitmapImage src = new BitmapImage();

            try
            {
                src.BeginInit();
                src.UriSource = new Uri(path);
                src.EndInit();
            }
            catch (UriFormatException)
            {
                return null;
            }
            return src;
        }

        /// <summary>
        /// Converts Image Source to string.
        /// </summary>
        /// <param name="value">
        /// ///
        /// </param>
        /// <param name="targetType">
        /// </param>
        /// <param name="parameter">
        /// </param>
        /// <param name="culture">
        /// </param>
        /// <returns>
        /// </returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => null;
    }
}
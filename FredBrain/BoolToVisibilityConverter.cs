using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FredBrain
{
    /// <summary>
    /// Represents the converter that converts a boolean value into a Visibility value.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value into a Visibility value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// If value is true then this method returns Visibility.Visible, otherwise Visiblity.Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                string param = parameter as string;
                if (param != null && param == "!")
                {
                    return (bool)value ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// This method is not supported and throws an exception when it is called.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Nothing because this method throws an exception.</returns>
        /// <exception cref="NotSupportedException">Throws this exception when the method is called.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

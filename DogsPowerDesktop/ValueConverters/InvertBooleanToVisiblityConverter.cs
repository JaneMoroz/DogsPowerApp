using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace DogsPowerDesktop
{
    /// <summary>
    /// A converter that takes in a boolean and returns a <see cref="Visibility"/>
    /// </summary>
    public class InvertBooleanToVisiblityConverter : BaseValueConverter<InvertBooleanToVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
                return (bool)value ? Visibility.Hidden : Visibility.Visible;
            else
                // If parameter is not null set to collapsed
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

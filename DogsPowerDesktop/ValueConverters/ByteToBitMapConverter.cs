using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace DogsPowerDesktop
{
    public class ByteToBitMapConverter : BaseValueConverter<ByteToBitMapConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                using (var stream = new MemoryStream(value as byte[]))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
            else
            {
                var image = new BitmapImage(new Uri("pack://application:,,,/Images/Samples/Catisimo.jpg"));
                return image;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

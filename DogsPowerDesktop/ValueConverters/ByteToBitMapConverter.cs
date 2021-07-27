using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DogsPowerDesktop
{
    public class ByteToBitMapConverter : BaseValueConverter<ByteToBitMapConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                // Convert byte array to image
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
                // If there is no parameter
                if (parameter == null)
                // Return cat image
                {
                    var image = new BitmapImage(new Uri("pack://application:,,,/Images/Samples/Catisimo.jpg"));
                    return image;
                }
                // if there is a parameter
                else
                // Return transparent image
                {
                    int width = 128;
                    int height = width;
                    int stride = width / 8;
                    byte[] pixels = new byte[height * stride];

                    // Try creating a new image with a custom palette.
                    List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
                    colors.Add(Colors.Transparent);
                    BitmapPalette myPalette = new BitmapPalette(colors);

                    // Creates a new empty image with the pre-defined palette
                    BitmapSource image = BitmapSource.Create(
                                                             width, height,
                                                             96, 96,
                                                             PixelFormats.Indexed1,
                                                             myPalette,
                                                             pixels,
                                                             stride);
                    return image;
                }

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

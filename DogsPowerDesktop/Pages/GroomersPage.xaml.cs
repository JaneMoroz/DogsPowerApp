using DogsPowerDesktop.Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DogsPowerDesktop
{
    /// <summary>
    /// Логика взаимодействия для GroomersPage.xaml
    /// </summary>
    public partial class GroomersPage : BasePage<GroomersViewModel>
    {
        public GroomersPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public GroomersPage(GroomersViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select an image";
            dialog.Filter = "JPG(.jpg)|*.jpg|PNG(.png)|*.png|JPEG(.jpeg)|*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                var bitmapImage = new BitmapImage(new Uri(dialog.FileName));
                ViewModel.SelectedGroomer.ProfilePicture = BitmapToByteArray(bitmapImage);
                ViewModel.ProfilePicture = BitmapToByteArray(bitmapImage);
            }
        }

        /// <summary>
        /// From bitmap to byte converter
        /// </summary>
        /// <param name="imageC"></param>
        /// <returns></returns>
        public byte[] BitmapToByteArray(BitmapImage bitmapImage)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memStream);
                var output = memStream.ToArray();
                return output;
            }
            
        }
    }
}

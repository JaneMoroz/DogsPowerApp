using DogsPowerDesktop.Library;
using System;
using System.Collections.Generic;
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
    }
}

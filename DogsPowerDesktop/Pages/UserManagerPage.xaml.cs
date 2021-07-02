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
    /// Логика взаимодействия для UserManagerPage.xaml
    /// </summary>
    public partial class UserManagerPage : BasePage<UserManagerViewModel>
    {
        public UserManagerPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public UserManagerPage(UserManagerViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}

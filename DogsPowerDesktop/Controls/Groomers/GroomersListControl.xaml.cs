using DogsPowerDesktop.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для GroomersListControl.xaml
    /// </summary>
    public partial class GroomersListControl : UserControl
    {
        public GroomersListControl()
        {
            InitializeComponent();

            // If we are in design mode...
            if (DesignerProperties.GetIsInDesignMode(this))
                // Create new instance of appointment view model
                DataContext = new GroomersListViewModel();
            else
                DataContext = IoC.TodayGroomersList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// Today's date
        /// </summary>
        public string TodaysDate => $"{DateTimeOffset.Now.ToString("dddd, MMMM dd", new CultureInfo("en-US"))}";

        /// <summary>
        /// True if appointment control should be visible
        /// </summary>
        public bool AppointmentVisible { get; set; } = false;

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open appointment control
        /// </summary>
        public ICommand OpenAppointmentCommand { get; set; }

        /// <summary>
        /// The command to close appointment control
        /// </summary>
        public ICommand CloseAppointmentCommand { get; set; }

        #endregion

        #region Constructor

        public ApplicationViewModel()
        {
            // Create the commands
            OpenAppointmentCommand = new RelayCommand(OpenAppointment);
            CloseAppointmentCommand = new RelayCommand(CloseAppointment);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open appointment control
        /// </summary>
        public void OpenAppointment()
        {
            // Open schedule appointment control
            AppointmentVisible = true;
        }

        /// <summary>
        /// Close appointment control
        /// </summary>
        public void CloseAppointment()
        {
            // Close schedule appointment control
            AppointmentVisible = false;
        }

        #endregion

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Set the view model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;

            // Fire off a CurrentPage changed event
            OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not?
            SideMenuVisible = page == ApplicationPage.Main;

        }
    }
}

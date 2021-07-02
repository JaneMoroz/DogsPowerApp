using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for User Manager Page
    /// </summary>
    public class UserManagerViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of users
        /// </summary>
        public List<UserDetailsModel> Users { get; set; }

        public UserDetailsModel SelectedUser { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open user manager page
        /// </summary>
        public ICommand OpenUserManagerCommand { get; set; }

        /// <summary>
        /// The command to close user manager page
        /// </summary>
        public ICommand CloseUserManagerCommand { get; set; }

        #endregion

        #region Constructor

        public UserManagerViewModel()
        {
            // Create the commands
            OpenUserManagerCommand = new RelayCommand(OpenUserManager);
            CloseUserManagerCommand = new RelayCommand(CloseUserManager);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open user manager page
        /// </summary>
        public void OpenUserManager()
        {
            // Open user manager page
            IoC.Application.GoToPage(ApplicationPage.UserManager);
        }

        /// <summary>
        /// Close user manager page
        /// </summary>
        public void CloseUserManager()
        {
            // Close user manager page
            IoC.Application.GoToPage(ApplicationPage.Main);
        }

        #endregion
    }
}

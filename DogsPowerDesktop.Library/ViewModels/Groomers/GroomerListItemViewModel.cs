using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    ///  A View Model for GroomerListItemControl
    /// </summary>
    public class GroomersListItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Groomer's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Groomer's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Groomer's full name
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Groomer's photo
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// True if groomer is selected
        /// </summary>
        public bool IsSelected { get; set; } = false;

        #endregion

        #region Commands

        /// <summary>
        /// Opens the current message thread
        /// </summary>
        public ICommand OpenScheduleCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// /// <summary>
        /// Default constructor
        /// </summary>
        /// </summary>
        public GroomersListItemViewModel()
        {
            // Create commands
            OpenScheduleCommand = new RelayCommand(OpenSchedule);
        }

        #endregion

        #region Command Methods

        public void OpenSchedule()
        {
            if (IsSelected == true)
                IsSelected = false;
            else IsSelected = true;
        }

        #endregion
    }
}

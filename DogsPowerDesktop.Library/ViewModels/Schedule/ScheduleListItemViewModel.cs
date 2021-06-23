﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A View Model for ScheduleListItemControl
    /// </summary>
    public class ScheduleListItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Start time of the appointment
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Duration of the service Choosen
        /// </summary>
        public TimeSpan ServiceDuration { get; set; }

        /// <summary>
        /// End time of the appointment
        /// </summary>
        public DateTimeOffset EndTime => StartTime.Add(ServiceDuration);

        public string Time => $"{StartTime.UtcDateTime:HH:mm} - {EndTime.UtcDateTime:HH:mm}";

        /// <summary>
        /// Customers first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer's  full name
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
        
        /// <summary>
        /// A pet's name
        /// </summary>
        public string PetName { get; set; }

        /// <summary>
        /// Name of the service
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// A pet's weight
        /// </summary>
        public string Weight { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ScheduleListItemViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public void Edit()
        {

        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public void Delete()
        {
            
        }

        #endregion
    }
}

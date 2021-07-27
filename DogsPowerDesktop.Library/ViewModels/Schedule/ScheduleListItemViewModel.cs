using System;
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
        /// Appointment Id from db
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Start time of the appointment
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Duration of the service Choosen
        /// </summary>
        public TimeSpan ServiceDuration { get; set; }

        /// <summary>
        /// End time of the appointment
        /// </summary>
        public TimeSpan EndTime => StartTime.Add(ServiceDuration);

        public string Time => string.Format("{0:00}:{1:00}-{2:00}:{3:00}", StartTime.Hours, StartTime.Minutes, EndTime.Hours, EndTime.Minutes);

        /// <summary>
        /// Time Now
        /// </summary>
        public DateTimeOffset CurrentTime => DateTimeOffset.Now;

        /// <summary>
        /// Determines whether the appointment is being taken place
        /// </summary>
        public bool InProgress => ((CurrentTime.Hour == StartTime.Hours && CurrentTime.Minute > StartTime.Minutes) || (CurrentTime.Hour >= StartTime.Hours))
            && ((CurrentTime.Hour == EndTime.Hours && CurrentTime.Minute < EndTime.Minutes) || CurrentTime.Hour < EndTime.Hours);

        /// <summary>
        /// Determines whether the appointment has finished 
        /// </summary>
        public bool IsOver => (CurrentTime.Hour == EndTime.Hours && CurrentTime.Minute > EndTime.Minutes)
            || (CurrentTime.Hour > EndTime.Hours);

        /// <summary>
        /// The RGB values (in hex) for the background color of the appointment
        /// </summary>  
        public string BackgroundRGB
        {
            get 
            {
                if (InProgress)
                    return "FFA6A6";
                else if (IsOver)
                    return "9FA1A1";
                else
                    return "80D9C2";
            }
        }

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
        /// The command for when the user wants to edit an appointment
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// The command for when the user wants to delete an appointment
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
        /// Edits an appointment
        /// </summary>
        public void Edit()
        {

        }

        /// <summary>
        /// Deletes an appointment
        /// </summary>
        public void Delete()
        {
            IoC.UI.ShowMessage(new MessageBoxDialogViewModel
            {
                Title = "Delete",
                OkText = "Yes",
                NotOkText = "No",
                Message = "Are you sure you want to delete this appointment?"
            });
        }

        #endregion
    }
}

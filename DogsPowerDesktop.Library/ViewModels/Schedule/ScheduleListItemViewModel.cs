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
        /// Time Now
        /// </summary>
        public DateTimeOffset CurrentTime => DateTimeOffset.Now;

        /// <summary>
        /// Determines whether the appointment is being taken place
        /// </summary>
        public bool InProgress => ((CurrentTime.Hour == StartTime.Hour && CurrentTime.Minute > StartTime.Minute) || (CurrentTime.Hour >= StartTime.Hour))
            && ((CurrentTime.Hour == EndTime.Hour && CurrentTime.Minute < EndTime.Minute) || CurrentTime.Hour < EndTime.Hour);

        /// <summary>
        /// Determines whether the appointment has finished 
        /// </summary>
        public bool IsOver => (CurrentTime.Hour == EndTime.Hour && CurrentTime.Minute > EndTime.Minute)
            || (CurrentTime.Hour > EndTime.Hour);

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

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
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

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

            if (FirstName == "Mary")
            {
                IoC.Application.GoToPage(ApplicationPage.Main, new ScheduleListViewModel
                {
                    List = new List<ScheduleListItemViewModel>
                    {
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 17, 40, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(1, 30, 0),
                            FirstName = "Jay",
                            LastName = "Smith",
                            PetName = "Sunny",
                            ServiceName = "Basic Full Groom",
                            Weight = "120.0 - 134.9"
                        },
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 19, 30, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(1, 15, 0),
                            FirstName = "Carry",
                            LastName = "Russel",
                            PetName = "Chanel",
                            ServiceName = "Basic Full Groom",
                            Weight = "15 - 29.9"
                        },
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 20, 50, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(2, 0, 0),
                            FirstName = "Matt",
                            LastName = "Mattews",
                            PetName = "Ro",
                            ServiceName = "Basic Full Groom",
                            Weight = "60.0 - 74.9"
                        }
                    }
                });
            }

            else
            {
                IoC.Application.GoToPage(ApplicationPage.Main, new ScheduleListViewModel
                {
                    List = new List<ScheduleListItemViewModel>
                    {
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 9, 0, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(1, 30, 0),
                            FirstName = "Kate",
                            LastName = "King",
                            PetName = "Rocky",
                            ServiceName = "Basic Full Groom",
                            Weight = "120.0 - 134.9"
                        },
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 11, 0, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(1, 15, 0),
                            FirstName = "Mark",
                            LastName = "Clark",
                            PetName = "Coco",
                            ServiceName = "Basic Full Groom",
                            Weight = "15 - 29.9"
                        },
                        new ScheduleListItemViewModel
                        {
                            StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 13, 0, 0, TimeSpan.Zero),
                            ServiceDuration = new TimeSpan(2, 0, 0),
                            FirstName = "Cathy",
                            LastName = "Florance",
                            PetName = "Zeus",
                            ServiceName = "Basic Full Groom",
                            Weight = "60.0 - 74.9"
                        }
                    }
                });
            }
        }

        #endregion
    }
}

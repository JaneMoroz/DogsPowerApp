using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        /// Groomer's Id
        /// </summary>
        public string Id { get; set; }

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
        public byte[] Image { get; set; }

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
            OpenScheduleCommand = new RelayParameterizedCommand(async async => await OpenScheduleAsync());
        }

        #endregion

        #region Command Methods

        public async Task OpenScheduleAsync()
        {
            //Load selected groomer schedule
            await IoC.Groomers.LoadSelectedGroomerSchedule(Id);

            IoC.Application.GoToPage(ApplicationPage.Main, IoC.GroomerScheduleList);

        //    if (FirstName == "Terry")
        //    {
        //        IoC.Application.GoToPage(ApplicationPage.Main, new ScheduleListViewModel
        //        {
        //            List = new List<ScheduleListItemViewModel>
        //            {
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(9, 30, 0),
        //                    ServiceDuration = new TimeSpan(1, 30, 0),
        //                    FirstName = "Jay",
        //                    LastName = "Smith",
        //                    PetName = "Sunny",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "120.0 - 134.9"
        //                },
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(12, 30, 0),
        //                    ServiceDuration = new TimeSpan(1, 15, 0),
        //                    FirstName = "Carry",
        //                    LastName = "Russel",
        //                    PetName = "Chanel",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "15 - 29.9"
        //                },
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(15, 30, 0),
        //                    ServiceDuration = new TimeSpan(2, 0, 0),
        //                    FirstName = "Matt",
        //                    LastName = "Mattews",
        //                    PetName = "Ro",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "60.0 - 74.9"
        //                }
        //            }
        //        });
        //    }

        //    else
        //    {
        //        IoC.Application.GoToPage(ApplicationPage.Main, new ScheduleListViewModel
        //        {
        //            List = new List<ScheduleListItemViewModel>
        //            {
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(10, 30, 0),
        //                    ServiceDuration = new TimeSpan(1, 30, 0),
        //                    FirstName = "Kate",
        //                    LastName = "King",
        //                    PetName = "Rocky",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "120.0 - 134.9"
        //                },
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(12, 30, 0),
        //                    ServiceDuration = new TimeSpan(1, 15, 0),
        //                    FirstName = "Mark",
        //                    LastName = "Clark",
        //                    PetName = "Coco",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "15 - 29.9"
        //                },
        //                new ScheduleListItemViewModel
        //                {
        //                    StartTime = new TimeSpan(17, 30, 0),
        //                    ServiceDuration = new TimeSpan(2, 0, 0),
        //                    FirstName = "Cathy",
        //                    LastName = "Florance",
        //                    PetName = "Zeus",
        //                    ServiceName = "Basic Full Groom",
        //                    Weight = "60.0 - 74.9"
        //                }
        //            }
        //        });
        //    }
        }

        #endregion
    }
}

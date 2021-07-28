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
            var groomerSchedule = await IoC.Groomers.LoadSelectedGroomerSchedule(Id);

            // Create new schedule list
            var scheduleList = new ScheduleListViewModel
            {
                List = new List<ScheduleListItemViewModel>()
            };

            if (groomerSchedule.Count != 0)
            {
                // Add each appointment to schedule list
                foreach (var s in groomerSchedule)
                {
                    var appointment = new ScheduleListItemViewModel
                    {
                        StartTime = s.StartTime,
                        ServiceDuration = s.Duration,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        PetName = s.Name,
                        ServiceName = s.ServiceName,
                        Weight = s.WeightName
                    };
                    scheduleList.List.Add(appointment);
                }
            }

            IoC.Application.GoToPage(ApplicationPage.Main, scheduleList);
        }

        #endregion
    }
}

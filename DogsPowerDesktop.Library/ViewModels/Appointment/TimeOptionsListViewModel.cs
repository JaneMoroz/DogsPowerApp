using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for TimeOptionsListControl
    /// </summary>
    public class TimeOptionsListViewModel : BaseViewModel
    {
        /// <summary>
        ///  List of appointments
        /// </summary>
        public List<TimeOptionsListItemViewModel> List { get; set; }

        private TimeOptionsListItemViewModel _selectedOption;
        public TimeOptionsListItemViewModel SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                _selectedOption = value;
                IoC.Appointment.Time = _selectedOption.AvailableTime;
            }
        }
    }
}

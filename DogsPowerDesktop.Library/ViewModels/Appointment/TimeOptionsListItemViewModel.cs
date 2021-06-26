using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for TimeOptionsListItemControl
    /// </summary>
    public class TimeOptionsListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// Availbale time option
        /// </summary>
        public DateTimeOffset AvailableTime { get; set; }

        public string Time => $"{AvailableTime.UtcDateTime:HH:mm}";
    }
}

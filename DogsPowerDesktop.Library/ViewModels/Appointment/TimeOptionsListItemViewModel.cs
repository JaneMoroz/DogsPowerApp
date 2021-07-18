using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for TimeOptionsListItemControl
    /// </summary>
    public class TimeOptionsListItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// Availbale time option
        /// </summary>
        public TimeSpan AvailableTime { get; set; }

        public string Time => string.Format("{0:00}:{1:00}", AvailableTime.Hours, AvailableTime.Minutes);

        #endregion
    }
}

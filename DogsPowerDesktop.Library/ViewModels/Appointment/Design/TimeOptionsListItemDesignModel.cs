using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="TimeOptionsListItemViewModel"/>
    /// </summary>
    public class TimeOptionsListItemDesignModel : TimeOptionsListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static TimeOptionsListItemDesignModel Instance => new TimeOptionsListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeOptionsListItemDesignModel()
        {
            var dateNow = DateTime.Now;
            AvailableTime = new DateTimeOffset(dateNow.Year, dateNow.Month, dateNow.Day, 9, 30, 0, TimeSpan.Zero);
        }

        #endregion
    }
}

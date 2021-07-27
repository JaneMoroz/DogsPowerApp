using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="ScheduleListItemViewModel"/>
    /// </summary>
    public class ScheduleListItemDesignModel : ScheduleListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ScheduleListItemDesignModel Instance => new ScheduleListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScheduleListItemDesignModel()
        {
            var dateNow = DateTime.Now.ToLocalTime();
            StartTime = new TimeSpan(9, 30, 0);
            ServiceDuration = new TimeSpan(1, 30, 0);
            FirstName = "Lucy";
            LastName = "King";
            PetName = "Rocky";
            ServiceName = "Basic Full Groom";
            Weight = "120.0 - 134.9";
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="TimeOptionsListViewModel"/>
    /// </summary>
    public class TimeOptionsListDesignModel : TimeOptionsListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static TimeOptionsListDesignModel Instance => new TimeOptionsListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TimeOptionsListDesignModel()
        {
            List = new List<TimeOptionsListItemViewModel>
            {
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(09, 30, 00)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(10, 30, 00)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(12, 30, 00)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(13, 00, 00)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(15, 00, 00)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new TimeSpan(18, 30, 00)
                },
            };
        }

        #endregion
    }
}

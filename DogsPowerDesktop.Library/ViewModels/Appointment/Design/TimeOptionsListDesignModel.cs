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
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0, TimeSpan.Zero)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0, TimeSpan.Zero)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0, TimeSpan.Zero)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0, TimeSpan.Zero)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 30, 0, TimeSpan.Zero)
                },
                new TimeOptionsListItemViewModel
                {
                    AvailableTime = new DateTimeOffset(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 45, 0, TimeSpan.Zero)
                }
            };
        }

        #endregion
    }
}

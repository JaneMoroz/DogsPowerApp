using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="ScheduleListViewModel"/>
    /// </summary>
    public class ScheduleListDesignModel : ScheduleListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ScheduleListDesignModel Instance => new ScheduleListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScheduleListDesignModel()
        {
            List = new List<ScheduleListItemViewModel>
            {
                new ScheduleListItemViewModel
                {
                    StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 9, 0, 0, TimeSpan.Zero),
                    ServiceDuration = new TimeSpan(1, 30, 0),
                    FirstName = "Lucy",
                    LastName = "King",
                    PetName = "Rocky",
                    ServiceName = "Basic Full Groom",
                    Weight = "120.0 - 134.9"
                },
                new ScheduleListItemViewModel
                {
                    StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 11, 0, 0, TimeSpan.Zero),
                    ServiceDuration = new TimeSpan(1, 15, 0),
                    FirstName = "Peter",
                    LastName = "Clark",
                    PetName = "Coco",
                    ServiceName = "Basic Full Groom",
                    Weight = "15 - 29.9"
                },
                new ScheduleListItemViewModel
                {
                    StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 13, 0, 0, TimeSpan.Zero),
                    ServiceDuration = new TimeSpan(2, 0, 0),
                    FirstName = "Anna",
                    LastName = "Florance",
                    PetName = "Zeus",
                    ServiceName = "Basic Full Groom",
                    Weight = "60.0 - 74.9"
                },
                new ScheduleListItemViewModel
                {
                    StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 15, 0, 0, TimeSpan.Zero),
                    ServiceDuration = new TimeSpan(2, 30, 0),
                    FirstName = "Ted",
                    LastName = "O'Conel",
                    PetName = "Phil",
                    ServiceName = "Basic Full Groom",
                    Weight = "90.0 - 104.9"
                },
                 new ScheduleListItemViewModel
                {
                    StartTime = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 17, 40, 0, TimeSpan.Zero),
                    ServiceDuration = new TimeSpan(1, 30, 0),
                    FirstName = "Jay",
                    LastName = "Smith",
                    PetName = "Suuny",
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
            };
        }

        #endregion
    }
}

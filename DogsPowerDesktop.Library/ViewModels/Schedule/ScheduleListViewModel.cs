using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A View model for ScheduleListControl
    /// </summary>
    public class ScheduleListViewModel : BaseViewModel
    {
        /// <summary>
        ///  List of appointments
        /// </summary>
        public List<ScheduleListItemViewModel> List { get; set; }
    }
}

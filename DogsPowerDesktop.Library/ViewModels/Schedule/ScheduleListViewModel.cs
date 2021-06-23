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
        public List<ScheduleListItemViewModel> List { get; set; }
    }
}

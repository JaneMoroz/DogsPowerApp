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
    }
}

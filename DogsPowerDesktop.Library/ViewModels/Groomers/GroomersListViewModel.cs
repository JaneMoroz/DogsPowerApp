using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for a list of groomers
    /// </summary>
    public class GroomersListViewModel : BaseViewModel
    {
        public List<GroomersListItemViewModel> Groomers { get; set; }
    }
}

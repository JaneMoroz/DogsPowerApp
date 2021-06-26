using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A View Model For An Appointment
    /// </summary>
    public class AppointmentViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Service choosen
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Pets weight
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Choosen date
        /// </summary>
        public DateTimeOffset Date { get; set; }
        
        /// <summary>
        /// Choosen time
        /// </summary>
        public DateTimeOffset Time { get; set; }

        /// <summary>
        /// Customer details
        /// </summary>
        public CustomerDetailsViewModel CustomerDetails { get; set; }

        #endregion

        #region Commands


        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppointmentViewModel()
        {
        }

        #endregion

        #region Command Methods

        #endregion
    }
}

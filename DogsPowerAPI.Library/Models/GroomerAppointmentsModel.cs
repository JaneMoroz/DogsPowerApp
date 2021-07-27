using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class GroomerAppointmentsModel
    {
        /// <summary>
        /// Appointment's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Appointment's Duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Appointment's Start time
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Customer's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer's pet name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Service name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public string WeightName { get; set; }
    }
}

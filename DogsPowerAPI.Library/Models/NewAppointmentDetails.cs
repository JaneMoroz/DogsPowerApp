using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class NewAppointmentDetails
    {
        /// <summary>
        /// Selected Service
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Selected weight option/pet's weight
        /// </summary>
        public string WeightOption { get; set; }

        /// <summary>
        /// Selected date
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Selected time
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        public string Email { get; set; }

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
        public string PetName { get; set; }

        /// <summary>
        /// Groomer Id
        /// </summary>
        public string GroomerId { get; set; }
    }
}

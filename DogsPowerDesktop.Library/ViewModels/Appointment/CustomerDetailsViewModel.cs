using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for CustomerDetailsControl
    /// </summary>
    public class CustomerDetailsViewModel : BaseViewModel
    {
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
        /// Customer's pet's name
        /// </summary>
        public string PetName { get; set; }
    }
}

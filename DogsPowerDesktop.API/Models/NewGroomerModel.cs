using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.API
{
    /// <summary>
    /// A model for a new groomer
    /// </summary>
    public class NewGroomerModel
    {
        /// <summary>
        /// Groomer's id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Groomer's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Groomer's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Groomer's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Groomer's email
        /// </summary>
        public string Email { get; set; }
    }
}

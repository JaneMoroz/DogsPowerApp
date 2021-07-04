using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsPowerAPI
{
    /// <summary>
    /// Information about user
    /// </summary>
    public class UserDetailsModel
    {
        /// <summary>
        /// User's Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // User's roles
        public List<string> Roles { get; set; } = new List<string>();
    }
}

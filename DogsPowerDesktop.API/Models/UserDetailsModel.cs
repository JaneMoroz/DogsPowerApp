using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DogsPowerDesktop.API
{
    /// <summary>
    /// Information about user
    /// </summary>
    public class UserDetailsModel
    {
        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // User's full name
        public string FullName => $"{FirstName} {LastName}";

        // User's roles
        public List<string> Roles { get; set; } = new List<string>();

        // User's roles in one line
        public string RoleList
        {
            get
            {
                return string.Join(", ", Roles);
            }
        }
    }
}

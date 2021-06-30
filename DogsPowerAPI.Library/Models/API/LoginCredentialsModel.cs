using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DogsPowerAPI.Library
{
    /// <summary>
    /// The credentials for  an API client to log into the server and receive a token back
    /// </summary>
    public class LoginCredentialsModel
    {
        #region Public Properties

        /// <summary>
        /// The users username or email
        /// </summary>
        [Required(ErrorMessage = "User Name Or Email is required")]
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// The users password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        #endregion
    }
}

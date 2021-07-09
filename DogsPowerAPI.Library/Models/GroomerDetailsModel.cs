﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class GroomerDetailsModel
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
        /// Groomer's full name
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Groomer's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Groomer's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Groomer's workdays
        /// </summary>
        public List<string> Workdays { get; set; }

        /// <summary>
        /// Indicates weather a groomer is active or not
        /// </summary>
        public bool IsActive { get; set; }
    }
}

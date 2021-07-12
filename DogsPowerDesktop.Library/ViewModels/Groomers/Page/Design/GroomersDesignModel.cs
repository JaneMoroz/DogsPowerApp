using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="GroomersViewModel"/>
    /// </summary>
    public class GroomersDesignModel : GroomersViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static GroomersDesignModel Instance => new GroomersDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroomersDesignModel()
        {
            Groomers = new List<GroomerDetailsModel>
            {
                new GroomerDetailsModel
                {
                    Username = "shepard",
                    FirstName = "Jack",
                    LastName = "Shepard",
                    Email = "contact@shepard.com",
                    Workdays = new List<string>{"Monday", "Wednesday"},
                    IsActive = true,
                    ProfilePicture = null
                },
                new GroomerDetailsModel
                {
                    Username = "eyre",
                    FirstName = "Jane",
                    LastName = "Eyre",
                    Email = "contact@eyre.com",
                    Workdays = new List<string>{"Thursday", "Friday", "Saturday", "Sunday"},
                    IsActive = false,
                    ProfilePicture = null
                },
                new GroomerDetailsModel
                {
                    Username = "redfield",
                    FirstName = "Clair",
                    LastName = "Redfield",
                    Email = "contact@redfield.com",
                    Workdays = new List<string>{"Monday", "Tuesday", "Thursday", "Friday"},
                    IsActive = true,
                    ProfilePicture = null
                },
                new GroomerDetailsModel
                {
                    Username = "liu",
                    FirstName = "Lucy",
                    LastName = "Liu",
                    Email = "contact@liu.com",
                    Workdays = new List<string>{"Friday", "Sunday"},
                    IsActive = true,
                    ProfilePicture = null
                },
                new GroomerDetailsModel
                {
                    Username = "potter",
                    FirstName = "Harry",
                    LastName = "Potter",
                    Email = "contact@potter.com",
                    Workdays = new List<string>{"Tuesday", "Saturday", "Sunday"},
                    IsActive = false,
                    ProfilePicture = null
                }
            };
        }
        #endregion
    }
}

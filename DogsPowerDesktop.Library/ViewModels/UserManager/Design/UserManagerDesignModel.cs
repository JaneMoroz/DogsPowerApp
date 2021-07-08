using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="UserManagerViewModel"/>
    /// </summary>
    public class UserManagerDesignModel : UserManagerViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static UserManagerDesignModel Instance => new UserManagerDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserManagerDesignModel()
        {
            Users = new ObservableCollection<UserDetailsModel>
            {
                new UserDetailsModel
                {
                    FirstName = "Anna",
                    LastName = "Martinez",
                    Roles = new List<string>{"Admin", "Manager"}
                },

                new UserDetailsModel
                {
                    FirstName = "Kerry",
                    LastName = "Russell",
                    Roles = new List<string>{"Manager"}
                },

                new UserDetailsModel
                {
                    FirstName = "Clair",
                    LastName = "Redfield",
                    Roles = new List<string>{"Groomer", "Manager"}
                },

                new UserDetailsModel
                {
                    FirstName = "Joel",
                    LastName = "Smith",
                    Roles = new List<string>{"Groomer"}
                },

                new UserDetailsModel
                {
                    FirstName = "Carl",
                    LastName = "Grimes",
                    Roles = new List<string>{"Groomer"}
                },

                new UserDetailsModel
                {
                    FirstName = "Terry",
                    LastName = "Preacher",
                    Roles = new List<string>{"Manager", "Groomer", "Admin"}
                },

                new UserDetailsModel
                {
                    FirstName = "Matt",
                    LastName = "Donovan",
                    Roles = new List<string>{"Manager"}
                }
            };
        }

        #endregion
    }
}

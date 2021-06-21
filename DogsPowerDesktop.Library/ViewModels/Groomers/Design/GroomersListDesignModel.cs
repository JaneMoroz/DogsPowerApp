using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="GroomersListViewModel"/>
    /// </summary>
    public class GroomersListDesignModel : GroomersListViewModel
    {

        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static GroomersListDesignModel Instance => new GroomersListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroomersListDesignModel()
        {
            Groomers = new List<GroomersListItemViewModel>
            {
                new GroomersListItemViewModel
                {
                    FirstName = "Mary",
                    LastName = "Parker",
                    Image = "/Images/Samples/Groomer1.png"
                },
                new GroomersListItemViewModel
                {
                    FirstName = "Rick",
                    LastName = "Martines",
                    Image = "/Images/Samples/Groomer2.png"
                },
                new GroomersListItemViewModel
                {
                    FirstName = "Terry",
                    LastName = "Johnes",
                    Image = "/Images/Samples/Groomer3.png"
                },
            };
        }

        #endregion
    }
}

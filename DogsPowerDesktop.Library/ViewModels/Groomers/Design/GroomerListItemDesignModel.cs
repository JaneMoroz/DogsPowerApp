using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The design-time data for a <see cref="GroomersListItemViewModel"/>
    /// </summary>
    public class GroomersListItemDesignModel : GroomersListItemViewModel
    {

        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static GroomersListItemDesignModel Instance => new GroomersListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GroomersListItemDesignModel()
        {
            FirstName = "Sarah";
            LastName = "Moretz";
            Image = null;
        }

        #endregion
    }
}

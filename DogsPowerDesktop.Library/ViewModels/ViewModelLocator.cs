using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// Locates view models from the IoC for use in binding in Xaml files
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Properties

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Application;

        /// <summary>
        /// The user manager view model
        /// </summary>
        public static UserManagerViewModel UserManagerViewModel => IoC.UserManager;

        /// <summary>
        /// The groomers view model
        /// </summary>
        public static GroomersViewModel GroomersViewModel => IoC.Groomers;

        #endregion
    }
}

using DogsPowerDesktop.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DogsPowerDesktop
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Login:
                    return new LoginPage(viewModel as LoginViewModel);

                case ApplicationPage.Main:
                    return new MainPage(viewModel as ScheduleListViewModel);

                case ApplicationPage.UserManager:
                    return new UserManagerPage(viewModel as UserManagerViewModel);

                case ApplicationPage.Groomers:
                    return new GroomersPage(viewModel as GroomersViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is MainPage)
                return ApplicationPage.Main;

            if (page is LoginPage)
                return ApplicationPage.Login;

            if (page is UserManagerPage)
                return ApplicationPage.UserManager;

            if (page is GroomersPage)
                return ApplicationPage.Groomers;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}

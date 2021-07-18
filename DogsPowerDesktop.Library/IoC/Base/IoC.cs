using DogsPowerDesktop.API;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The IoC container for our application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="Application"/>
        /// </summary>
        public static ApplicationViewModel Application => Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="UserManager"/>
        /// </summary>
        public static UserManagerViewModel UserManager => Get<UserManagerViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="GroomersViewModel"/>
        /// </summary>
        public static GroomersViewModel Groomers => Get<GroomersViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="AppointmentViewModel"/>
        /// </summary>
        public static AppointmentViewModel Appointment => Get<AppointmentViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => Get<IUIManager>();

        /// <summary>
        /// A shortcut to access the <see cref="IAPIHelper"/>
        /// </summary>
        public static IAPIHelper APIHelper => Get<IAPIHelper>();

        /// <summary>
        /// A shortcut to access the <see cref="IUserEndpoint"/>
        /// </summary>
        public static IUserEndpoint UserEndpoint => Get<IUserEndpoint>();

        /// <summary>
        /// A shortcut to access the <see cref="IGroomersEndpoint"/>
        /// </summary>
        public static IGroomersEndpoint GroomersEndpoint => Get<IGroomersEndpoint>();

        /// <summary>
        /// A shortcut to access the <see cref="IAppointmentEndpoint"/>
        /// </summary>
        public static IAppointmentEndpoint AppointmentEndpoint => Get<IAppointmentEndpoint>();
        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all 
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel()); // .ToSelf().InSingletonScope();
            Kernel.Bind<IAPIHelper>().ToConstant(new APIHelper());
            Kernel.Bind<IUserEndpoint>().ToConstant(new UserEndpoint(APIHelper));
            Kernel.Bind<IGroomersEndpoint>().ToConstant(new GroomersEndpoint(APIHelper));
            Kernel.Bind<IAppointmentEndpoint>().ToConstant(new AppointmentEndpoint(APIHelper));
            Kernel.Bind<UserManagerViewModel>().ToConstant(new UserManagerViewModel(UserEndpoint, GroomersEndpoint));
            Kernel.Bind<GroomersViewModel>().ToConstant(new GroomersViewModel(GroomersEndpoint));
            Kernel.Bind<AppointmentViewModel>().ToConstant(new AppointmentViewModel(AppointmentEndpoint));
        }

        #endregion

        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}

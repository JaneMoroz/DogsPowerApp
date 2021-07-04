using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The login of the user
        /// </summary>
        public string Login { get; set; } = "moroz";

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
        }

        #endregion

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // TODO: Fake a login...
                await Task.Delay(1000);

                try
                {
                    // Call the server and attempt to login with credentials
                    ApiResponse<AuthenticatedUserModel> response = await IoC.APIHelper.Authenticate(Login, (parameter as IHavePassword).SecurePassword.Unsecure());

                    // If there was no response, bad data, or  a response with a error message...
                    if (response == null || response.Response == null || (response as ApiResponse)?.Successful == false)
                    {
                        // Default error message
                        // TODO: Localize strings
                        var message = "";

                        // If we got a response from the server...
                        if (response.ErrorMessage != null)
                            // Set message to servers response
                            message = response.ErrorMessage;
                        // If we have a result but no server response details at all...
                        else if (response.ErrorMessage == null && response.Response == null)
                            message = "Unknown error from server call";
                        else
                            message = "Unable to login, tru again later";

                        // Display error
                        await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                        {
                            // TODO: Localize strings
                            Title = "Login Failed",
                            Message = message,
                            OkText = "Ok",
                            NotOkText = "Back"
                        });

                        // We are done
                        return;
                    }

                    // All is OK
                    // Load User Manager data
                    await IoC.UserManager.LoadAsync();
                    // Go to main page
                    IoC.Application.GoToPage(ApplicationPage.Main);
                }
                catch (Exception ex)
                {
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Login Failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                    return;

                }
            });
        }
    }
}

using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for User Manager Page
    /// </summary>
    public class UserManagerViewModel : BaseViewModel
    {
        #region Private Properties

        private readonly IUserEndpoint _userEndpoint;
        private readonly IGroomersEndpoint _groomersEndpoint;

        #endregion

        #region Public Properties

        /// <summary>
        /// List of users
        /// </summary>
        public ObservableCollection<UserDetailsModel> Users { get; set; } = new ObservableCollection<UserDetailsModel>();

        /// <summary>
        /// Selected user
        /// </summary>
        public UserDetailsModel SelectedUser { get; set; }

        /// <summary>
        /// List of all roles
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// List of selected user roles
        /// </summary>
        public List<string> UserRoles => SelectedUser.Roles;

        /// <summary>
        /// List of user roles to user in one line
        /// </summary>
        public string UserRolesList
        {
            get
            {
                if (UserRoles.Count > 0)
                    return string.Join(", ", UserRoles);
                else return "No roles availble";
            }
        }

        /// <summary>
        /// List of other available roles to user
        /// </summary>
        public List<string> AvailableRoles => Roles.Except(UserRoles).ToList();
        /// <summary>
        /// List of available roles to user in one line
        /// </summary>
        public string AvailableRolesList
        {
            get
            {
                if (AvailableRoles.Count > 0)
                    return string.Join(", ", AvailableRoles);
                else return "No roles availble";
            }
        }

        /// <summary>
        /// True if available roles list is not empty
        /// </summary>
        public bool CanAddRole => AvailableRoles.Count > 0;

        /// <summary>
        /// True if selected user's roles list is not empty
        /// </summary>
        public bool CanRemoveRole => UserRoles.Count > 0; 

        /// <summary>
        /// Selected role to add to selected user's roles
        /// </summary>
        public string SelectedRoleToAdd { get; set; }

        /// <summary>
        /// Selected role to remove from selected user's roles
        /// </summary>
        public string SelectedRoleToRemove { get; set; }

        /// <summary>
        /// Indicates if the user's role list is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// Opposite of edititing value
        /// </summary>
        public bool NotEditing => !Editing;

        /// <summary>
        /// Indicates if the user wants to add role
        /// </summary>
        public bool AddingRole { get; set; }

        /// <summary>
        /// Indicates if the user wants to remove role
        /// </summary>
        public bool RemovingRole { get; set; }

        /// <summary>
        /// Indicates if the user manager page is opened already
        /// </summary>
        public bool UserManagerIsOpen { get; set; }

        /// <summary>
        /// Username of a new user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// First name of a new user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of a new user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of a new user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A role choosen for a new user
        /// </summary>
        public string NewUserRoleSelected { get; set; }

        /// <summary>
        /// Indicates that a new user is being created
        /// </summary>
        public bool CreateIsRunning { get; set; }

        /// <summary>
        /// A new role to create
        /// </summary>
        public string NewRoleToCreate { get; set; }
        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open user manager page
        /// </summary>
        public ICommand OpenUserManagerCommand { get; set; }

        /// <summary>
        /// The command to close user manager page
        /// </summary>
        public ICommand CloseUserManagerCommand { get; set; }

        /// <summary>
        /// The command to add role
        /// </summary>
        public ICommand AddRoleCommand { get; set; }

        /// <summary>
        /// The command to remove role
        /// </summary>
        public ICommand RemoveRoleCommand { get; set; }

        /// <summary>
        /// The command to confirm action
        /// </summary>
        public ICommand ConfirmCommand { get; set; }

        /// <summary>
        /// The command to cancel action
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// The command to create a new user
        /// </summary>
        public ICommand CreateCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for design model
        /// </summary>
        public UserManagerViewModel()
        {
            // Create the commands
            OpenUserManagerCommand = new RelayCommand(OpenUserManager);
            CloseUserManagerCommand = new RelayCommand(CloseUserManager);
            AddRoleCommand = new RelayCommand(AddRole);
            RemoveRoleCommand = new RelayCommand(RemoveRole);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            CreateCommand = new RelayParameterizedCommand(async (parameter) => await CreateAsync(parameter));
        }

        public UserManagerViewModel(IUserEndpoint userEndpoint, IGroomersEndpoint groomersEndpoint) : this()
        {
            _userEndpoint = userEndpoint;
            _groomersEndpoint = groomersEndpoint;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open user manager page
        /// </summary>
        public void OpenUserManager()
        {
            // If user manager is not opened
            if (UserManagerIsOpen == false)
            {
                // Open user manager page
                IoC.Application.GoToPage(ApplicationPage.UserManager);
                // Set boolean to true
                UserManagerIsOpen = true;
            }
            // Else...
            else
            {
                // Close user manager page
                IoC.Application.GoToPage(ApplicationPage.Main);
                // Set boolean to false
                UserManagerIsOpen = false;
            }
            
        }

        /// <summary>
        /// Close user manager page
        /// </summary>
        public void CloseUserManager()
        {
            // If user manager page is already open
            if (UserManagerIsOpen == true)
            {
                // Close user manager page
                IoC.Application.GoToPage(ApplicationPage.Main);
                // Set boolean to false
                UserManagerIsOpen = false;
            }
        }

        /// <summary>
        /// Start adding a role
        /// </summary>
        public void AddRole()
        {
            Editing = true;
            AddingRole = true;
        }

        /// <summary>
        /// Start removing role
        /// </summary>
        public void RemoveRole()
        {
            Editing = true;
            RemovingRole = true;
        }

        /// <summary>
        /// Confirm adding or removing a role 
        /// </summary>
        public void Confirm()
        {
            Editing = false;
            if(AddingRole == true)
            {
                var selectedUser = SelectedUser;
                var roleToAdd = SelectedRoleToAdd;

                _userEndpoint.AddUserToRole(selectedUser.Id, roleToAdd);

                SelectedUser.Roles.Add(roleToAdd);

                OnPropertyChanged(nameof(UserRoles));
                OnPropertyChanged(nameof(AvailableRoles));
                OnPropertyChanged(nameof(UserRolesList));
                OnPropertyChanged(nameof(AvailableRolesList));
                OnPropertyChanged(nameof(CanAddRole));
                OnPropertyChanged(nameof(CanRemoveRole));

                AddingRole = false;
            }
            else
            {
                var selectedUser = SelectedUser;
                var roleToDelete = SelectedRoleToRemove;

                _userEndpoint.RemoveUserFromRole(selectedUser.Id, roleToDelete);

                SelectedUser.Roles.Remove(roleToDelete);

                OnPropertyChanged(nameof(UserRoles));
                OnPropertyChanged(nameof(AvailableRoles));
                OnPropertyChanged(nameof(UserRolesList));
                OnPropertyChanged(nameof(AvailableRolesList));
                OnPropertyChanged(nameof(CanAddRole));
                OnPropertyChanged(nameof(CanRemoveRole));

                RemovingRole = false;
            }

        }

        /// <summary>
        /// Cancel add or delete a role action
        /// </summary>
        public void Cancel()
        {
            Editing = false;
            AddingRole = false;
            RemovingRole = false;
        }

        /// <summary>
        /// Load data after successful login
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync()
        {
            // Load users
            Users = await _userEndpoint.GetAll();

            // Load roles
            Roles = await _userEndpoint.GetAllRoles();
        }

        /// <summary>
        /// Attempts to create a new user
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task CreateAsync(object parameter)
        {
            await RunCommandAsync(() => CreateIsRunning, async () =>
            {
                try
                {
                    // Call the server and attempt to register with credentials
                    ApiResponse<UserDetailsModel> response = await IoC.UserEndpoint.Create(Username, FirstName, LastName, Email, role: !string.IsNullOrEmpty(NewRoleToCreate) ? NewRoleToCreate : NewUserRoleSelected, password: (parameter as IHavePassword).SecurePassword.Unsecure());

                    // If there was no response, bad data, or  a response with a error message...
                    if (response == null || response.ErrorMessage != null || (response as ApiResponse)?.Successful == false)
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
                            message = "Unable to create a new user, try again later";

                        // Display error
                        await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                        {
                            // TODO: Localize strings
                            Title = "Creation Failed",
                            Message = message,
                            OkText = "Ok",
                            NotOkText = "Back"
                        });

                        // We are done
                        return;
                    }

                    // All is OK
                    // Check if a new user has a role of "Groomer"
                    if (NewUserRoleSelected == "Groomer")
                    {
                        try
                        {
                            // Add a new groomer to "Groomers" table
                            await _groomersEndpoint.AddAGroomer(response.Response.Id, FirstName, LastName, Username, Email);
                        }
                        catch (Exception ex)
                        {
                            // Display error
                            await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                            {
                                // TODO: Localize strings
                                Title = "Creation of a groomer failed",
                                Message = ex.Message,
                                OkText = "Ok",
                                NotOkText = "Back"
                            });
                        }
                    }

                    // Update user listbox
                    UpdateUsers(response.Response);

                    // Clear all fields
                    Username = "";
                    FirstName = "";
                    LastName = "";
                    Email = "";
                    NewRoleToCreate = "";
                    (parameter as IHavePassword).SecurePassword.Clear();

                    // Display a message
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Creation Succeded",
                        Message = "A new user's been created!",
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                }
                catch (Exception ex)
                {
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Creation Failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                    return;

                }
            });
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Update all fields, responsible for users, their roles and adding/removing roles
        /// </summary>
        /// <param name="newUser"></param>
        private void UpdateUsers(UserDetailsModel newUser)
        {
            // Add a new user to users list
            Users.Add(newUser);

            // Get new user's roles
            var newUserRoles = newUser.Roles;

            // Check if there are new roles
            foreach(var role in newUserRoles)
            {
                if (!Roles.Contains(role))
                {
                    Roles.Add(role);
                }
            }
        }

        #endregion
    }
}

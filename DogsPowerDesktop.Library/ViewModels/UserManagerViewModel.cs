using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DogsPowerDesktop.Library
{
    /// <summary>
    /// A view model for User Manager Page
    /// </summary>
    public class UserManagerViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of users
        /// </summary>
        public List<UserDetailsModel> Users { get; set; }

        /// <summary>
        /// Selected user
        /// </summary>
        public UserDetailsModel SelectedUser { get; set; }

        /// <summary>
        /// List of all roles
        /// </summary>
        public List<string> Roles { get; set; } = new List<string> { "Admin", "Manager", "Groomer" };

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
                else return "No roles availble left";
            }
        }

        /// <summary>
        /// List of other available roles to user
        /// </summary>
        public List<string> AvailableRoles => Roles.Except(UserRoles).ToList();

        /// <summary>
        /// List of available roles to user in one line
        /// </summary>
        public string AvailbaleRolesList
        {
            get
            {
                if (AvailableRoles.Count > 0)
                    return string.Join(", ", AvailableRoles);
                else return "No roles availble left";
            }
        }

        /// <summary>
        /// True if available roles list is not empty
        /// </summary>
        public bool CanAddRole => AvailableRoles.Count > 0;

        /// <summary>
        /// True if selected user's roles list is not empty
        /// </summary>
        public bool CanDeleteRole => UserRoles.Count > 0; 

        /// <summary>
        /// Selected role to add to selected user's roles
        /// </summary>
        public string SelectedRoleToAdd { get; set; }

        /// <summary>
        /// Selected role to delete from selected user's roles
        /// </summary>
        public string SelectedRoleToDelete { get; set; }

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
        /// Indicates if the user wants to delete role
        /// </summary>
        public bool DeletingRole { get; set; }

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
        /// The command to delete role
        /// </summary>
        public ICommand DeleteRoleCommand { get; set; }

        /// <summary>
        /// The command to confirm action
        /// </summary>
        public ICommand ConfirmCommand { get; set; }

        /// <summary>
        /// The command to cancel action
        /// </summary>
        public ICommand CancelCommand { get; set; }

        #endregion

        #region Constructor

        public UserManagerViewModel()
        {
            // Create the commands
            OpenUserManagerCommand = new RelayCommand(OpenUserManager);
            CloseUserManagerCommand = new RelayCommand(CloseUserManager);
            AddRoleCommand = new RelayCommand(AddRole);
            DeleteRoleCommand = new RelayCommand(DeleteRole);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open user manager page
        /// </summary>
        public void OpenUserManager()
        {
            // Open user manager page
            IoC.Application.GoToPage(ApplicationPage.UserManager);
        }

        /// <summary>
        /// Close user manager page
        /// </summary>
        public void CloseUserManager()
        {
            // Close user manager page
            IoC.Application.GoToPage(ApplicationPage.Main);
        }

        // Start adding a role
        public void AddRole()
        {
            Editing = true;
            AddingRole = true;
        }

        // Start deleting role
        public void DeleteRole()
        {
            Editing = true;
            DeletingRole = true;
        }

        // Confirm add or delete a role action
        public void Confirm()
        {
            Editing = false;
            AddingRole = false;
            DeletingRole = false;
        }

        // Cancel add or delete a role action
        public void Cancel()
        {
            Editing = false;
            AddingRole = false;
            DeletingRole = false;
        }

        #endregion
    }
}

﻿using DogsPowerDesktop.API;
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
    /// A view model for Groomers Page
    /// </summary>
    public class GroomersViewModel : BaseViewModel
    {
        /// <summary>
        /// List of groomers
        /// </summary>
        public ObservableCollection<GroomerDetailsModel> Groomers { get; set; } = new ObservableCollection<GroomerDetailsModel>();

        /// <summary>
        /// Selected groomer
        /// </summary>
        public GroomerDetailsModel SelectedGroomer { get; set; }

        /// <summary>
        /// List of all workdays
        /// </summary>
        public List<string> Workdays { get; set; } = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        /// <summary>
        /// Username of a selected groomer
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// First name of a selected groomer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of a selected groomer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of a selected groomer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Indicates weather a groomer is active or not
        /// </summary>
        public bool IsActive
        { 
            get
            {
                return SelectedGroomer.IsActive;
            }
            set
            {

            }
        }

        /// <summary>
        /// Selected groomer list of workdays
        /// </summary>
        public List<string> GroomerWorkdays => SelectedGroomer.Workdays;

        /// <summary>
        /// List of groomer workdays in one line
        /// </summary>
        public string GroomerWorkdaysList
        {
            get
            {
                if (GroomerWorkdays.Count > 0)
                    return string.Join(", ", GroomerWorkdays);
                else return "Workdays not set";
            }
        }

        /// <summary>
        /// List of other available workdays to groomer
        /// </summary>
        public List<string> AvailableWorkdays => Workdays.Except(GroomerWorkdays).ToList();
        /// <summary>
        /// List of available workdays to groomer in one line
        /// </summary>
        public string AvailableWorkdaysList
        {
            get
            {
                if (AvailableWorkdays.Count > 0)
                    return string.Join(", ", AvailableWorkdays);
                else return "No more workdays availble";
            }
        }

        /// <summary>
        /// True if available workdays list is not empty
        /// </summary>
        public bool CanAddWorkday => AvailableWorkdays.Count > 0;

        /// <summary>
        /// True if selected groomer's workdays list is not empty
        /// </summary>
        public bool CanRemoveWorkday => GroomerWorkdays.Count > 0;

        /// <summary>
        /// Selected workday to add to selected groomer's workdays
        /// </summary>
        public string SelectedWorkdayToAdd { get; set; }

        /// <summary>
        /// Selected Workdays to remove from selected groomer's workdays
        /// </summary>
        public string SelectedWorkdayToRemove { get; set; }

        /// <summary>
        /// Indicates if the groomer's workdays list is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// Opposite of edititing value
        /// </summary>
        public bool NotEditing => !Editing;

        /// <summary>
        /// Indicates if the user wants to add workday
        /// </summary>
        public bool AddingWorkday { get; set; }

        /// <summary>
        /// Indicates if the user wants to remove workday
        /// </summary>
        public bool RemovingWorkday { get; set; }

        /// <summary>
        /// Indicates if the groomers page is opened already
        /// </summary>
        public bool GroomersIsOpen { get; set; }

        /// <summary>
        /// Indicates that status changes is in proccess of saving
        /// </summary>
        public bool StatusIsSaving { get; set; }


        #region Public Commands

        /// <summary>
        /// The command to open groomers page
        /// </summary>
        public ICommand OpenGroomersCommand { get; set; }

        /// <summary>
        /// The command to close groomers page
        /// </summary>
        public ICommand CloseGroomersCommand { get; set; }

        /// <summary>
        /// The command to add workday
        /// </summary>
        public ICommand AddWorkdayCommand { get; set; }

        /// <summary>
        /// The command to remove workday
        /// </summary>
        public ICommand RemoveWorkdayCommand { get; set; }

        /// <summary>
        /// The command to confirm action
        /// </summary>
        public ICommand ConfirmCommand { get; set; }

        /// <summary>
        /// The command to cancel action
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// The command to choose whether a groomer is active or not
        /// </summary>
        public ICommand SetStatusCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for design model
        /// </summary>
        public GroomersViewModel()
        {
            // Create the commands
            OpenGroomersCommand = new RelayCommand(OpenGroomers);
            CloseGroomersCommand = new RelayCommand(CloseGroomers);
            AddWorkdayCommand = new RelayCommand(AddWorkday);
            RemoveWorkdayCommand = new RelayCommand(RemoveWorkday);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            SetStatusCommand = new RelayCommand(SetStatusAsync);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open groomers page
        /// </summary>
        public void OpenGroomers()
        {
            // If groomers is not opened
            if (GroomersIsOpen == false)
            {
                // Open groomers page
                IoC.Application.GoToPage(ApplicationPage.Groomers);
                // Set boolean to true
                GroomersIsOpen = true;
            }
            // Else...
            else
            {
                // Close groomers page
                IoC.Application.GoToPage(ApplicationPage.Main);
                // Set boolean to false
                GroomersIsOpen = false;
            }

        }

        /// <summary>
        /// Close groomers page
        /// </summary>
        public void CloseGroomers()
        {
            // If groomers page is already open
            if (GroomersIsOpen == true)
            {
                // Close user groomers page
                IoC.Application.GoToPage(ApplicationPage.Main);
                // Set boolean to false
                GroomersIsOpen = false;
            }
        }

        /// <summary>
        /// Start adding a workday
        /// </summary>
        public void AddWorkday()
        {
            Editing = true;
            AddingWorkday = true;
        }

        /// <summary>
        /// Start removing workday
        /// </summary>
        public void RemoveWorkday()
        {
            Editing = true;
            RemovingWorkday = true;
        }

        /// <summary>
        /// Confirm adding or removing a workday 
        /// </summary>
        public void Confirm()
        {
            Editing = false;
            if (AddingWorkday == true)
            {
                var selectedGroomer = SelectedGroomer;
                var workdayToAdd = SelectedWorkdayToAdd;

                //_userEndpoint.AddUserToRole(selectedGroomer.Id, workdayToAdd);

                selectedGroomer.Workdays.Add(workdayToAdd);

                OnPropertyChanged(nameof(GroomerWorkdays));
                OnPropertyChanged(nameof(AvailableWorkdays));
                OnPropertyChanged(nameof(GroomerWorkdaysList));
                OnPropertyChanged(nameof(AvailableWorkdaysList));
                OnPropertyChanged(nameof(CanAddWorkday));
                OnPropertyChanged(nameof(CanRemoveWorkday));

                AddingWorkday = false;
            }
            else
            {
                var selectedGroomer = SelectedGroomer;
                var workdayToDelete = SelectedWorkdayToRemove;

                //_userEndpoint.RemoveUserFromRole(selectedGroomer.Id, workdayToDelete);

                selectedGroomer.Workdays.Remove(workdayToDelete);

                OnPropertyChanged(nameof(GroomerWorkdays));
                OnPropertyChanged(nameof(AvailableWorkdays));
                OnPropertyChanged(nameof(GroomerWorkdaysList));
                OnPropertyChanged(nameof(AvailableWorkdaysList));
                OnPropertyChanged(nameof(CanAddWorkday));
                OnPropertyChanged(nameof(CanRemoveWorkday));

                RemovingWorkday = false;
            }

        }

        /// <summary>
        /// Cancel add or delete a workday action
        /// </summary>
        public void Cancel()
        {
            Editing = false;
            AddingWorkday = false;
            RemovingWorkday = false;
        }

        /// <summary>
        /// Choose whether a groomer is active or not
        /// </summary>
        public void SetStatusAsync()
        {
            //await RunCommandAsync(() => StatusIsSaving, async () =>
            //{
            //    if (IsActive)
            //    {
            //        IsActive = false;
            //    }
            //    else
            //        IsActive = true;

            //    //TODO: Save changes (workdays and isActive to database)

            //    // Update fields
            //    var selectedGroomer = SelectedGroomer;

            //    selectedGroomer.IsActive = IsActive;
            //});
            

            
        }

        /// <summary>
        /// Load data after successful login
        /// </summary>
        /// <returns></returns>
        public void LoadAsync()
        {
            // Load users
            //Groomers = await _userEndpoint.GetAll();

            // Load roles
            //Workdays = await _userEndpoint.GetAllRoles();
        }

        #endregion
    }
}

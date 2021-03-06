using DogsPowerDesktop.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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
        #region Private Properties

        private readonly IGroomersEndpoint _groomersEndpoint;

        #endregion

        #region Public Properties

        /// <summary>
        /// List of groomers
        /// </summary>
        public List<GroomerDetailsModel> Groomers { get; set; } = new List<GroomerDetailsModel>();

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
        public string Username => SelectedGroomer.Username;

        /// <summary>
        /// First name of a selected groomer
        /// </summary>
        public string FirstName => SelectedGroomer.FirstName;

        /// <summary>
        /// Last name of a selected groomer
        /// </summary>
        public string LastName => SelectedGroomer.LastName;

        /// <summary>
        /// Email of a selected groomer
        /// </summary>
        public string Email => SelectedGroomer.Email;

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
        /// Profile Picture
        /// </summary>
        public byte[] ProfilePicture
        { 
            get 
            {
                if (SelectedGroomer.ProfilePicture != null)
                    return SelectedGroomer.ProfilePicture;
                else
                    return null;
            }
            set
            {
                OnPropertyChanged(nameof(ProfilePicture));
            }
        }

        /// <summary>
        /// Selected groomer list of workdays
        /// </summary>
        public ObservableCollection<string> GroomerWorkdays => new ObservableCollection<string>(SelectedGroomer.Workdays);

        /// <summary>
        /// List of groomer workdays in one line
        /// </summary>
        public string GroomerWorkdaysList
        {
            get
            {
                if (GroomerWorkdays.Count > 1)
                    return string.Join(", ", GroomerWorkdays);
                else if (GroomerWorkdays.Count == 1 && !string.IsNullOrEmpty(GroomerWorkdays[0]))
                    return string.Join(", ", GroomerWorkdays);
                else return "Workdays not set";
            }
        }

        /// <summary>
        /// List of other available workdays to groomer
        /// </summary>
        public ObservableCollection<string> AvailableWorkdays => new ObservableCollection<string>(Workdays.Except(GroomerWorkdays).ToList());
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

        #endregion

        #region Transactional Properties

        /// <summary>
        /// Indicates if the groomers page is in the process of opening
        /// </summary>
        public bool GroomersIsOpening { get; set; }

        /// <summary>
        /// Indicates that saving is in progress
        /// </summary>
        public bool SavingChanges { get; set; }

        #endregion

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
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// The command to choose whether a groomer is active or not
        /// </summary>
        public ICommand SetStatusCommand { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for design model
        /// </summary>
        public GroomersViewModel()
        {
            // Create the commands
            OpenGroomersCommand = new RelayParameterizedCommand(async async => await OpenGroomersAsync());
            CloseGroomersCommand = new RelayCommand(CloseGroomers);
            AddWorkdayCommand = new RelayCommand(AddWorkday);
            RemoveWorkdayCommand = new RelayCommand(RemoveWorkday);
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayParameterizedCommand(async async => await SaveAsync());
            SetStatusCommand = new RelayCommand(SetStatus);
        }

        public GroomersViewModel(IGroomersEndpoint groomersEndpoint) : this()
        {
            _groomersEndpoint = groomersEndpoint;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Open groomers page
        /// </summary>
        public async Task OpenGroomersAsync()
        {
            // If groomers is not opened
            if (GroomersIsOpen == false)
            {
                await RunCommandAsync(() => GroomersIsOpening, async () =>
                {
                    // Load data
                    await LoadAsync();
                    // Open groomers page
                    IoC.Application.GoToPage(ApplicationPage.Groomers);
                    // Set boolean to true
                    GroomersIsOpen = true;
                });
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
        /// Set status to active or mot
        /// </summary>
        public void SetStatus()
        {
            if(IsActive == false)
            {
                SelectedGroomer.IsActive = true;
                OnPropertyChanged(nameof(IsActive));
            }
            else
            {
                SelectedGroomer.IsActive = false;
                OnPropertyChanged(nameof(IsActive));
            }
        }

        /// <summary>
        /// Save all changes to database
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await RunCommandAsync(() => SavingChanges, async () =>
            {
                try
                {
                    // Try to save changes to database
                    await _groomersEndpoint.UpdateWorkdays(SelectedGroomer.Id, SelectedGroomer.Workdays);
                    await _groomersEndpoint.UploadPicture(SelectedGroomer.Id, SelectedGroomer.ProfilePicture);
                    await _groomersEndpoint.UpdateStatus(SelectedGroomer.Id, SelectedGroomer.IsActive);

                    // Update UI


                    // All is well
                    // Show message
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Saving changes",
                        Message = "Changes are saved to database.",
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                }
                catch (Exception ex)
                {
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Saving failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                }
            });

            // Update Groomers on Main Page
            await LoadTodayGroomers();
        }

        /// <summary>
        /// Load data when the groomers page is being open
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync()
        {
            Groomers = await _groomersEndpoint.GetAllGroomersAllDetails();
        }

        /// <summary>
        /// Load groomers who work today
        /// </summary>
        /// <returns></returns>
        public async Task LoadTodayGroomers()
        {
            IoC.TodayGroomersList.Groomers = new List<GroomersListItemViewModel>();

            // Get todays day of the week
            // TODO: changed for fixed date for testing, need to be changed back when done
            //var todayWeekday = DateTimeOffset.Now.AddDays(1).DayOfWeek.ToString();
            var todayWeekday = new DateTimeOffset(2021, 7, 29, 0, 0, 0, TimeSpan.Zero).DayOfWeek.ToString();

            // Get all groomers who work this day
            var groomers = await _groomersEndpoint.GetGroomersByWeekday(todayWeekday);


            foreach (var g in groomers)
            {
                var groomer = new GroomersListItemViewModel
                {
                    Id = g.Id,
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Image = g.Picture
                };
                IoC.TodayGroomersList.Groomers.Add(groomer);
            }
        }

        /// <summary>
        /// Load selected groomer schedule
        /// </summary>
        /// <param name="groomerId"></param>
        /// <returns></returns>
        public async Task<List<GroomerAppointmentsModel>> LoadSelectedGroomerSchedule(string groomerId)
        {
            // Get today's date
            // TODO: changed for fixed date for testing, need to be changed back when done
            var todayDate = new DateTimeOffset(2021, 7, 29, 0, 0, 0, TimeSpan.Zero);
            // Get groomer's schedule
            var schedule = await _groomersEndpoint.GetGroomerAppointments(groomerId, todayDate);

            return schedule;
        }

        #endregion
    }
}

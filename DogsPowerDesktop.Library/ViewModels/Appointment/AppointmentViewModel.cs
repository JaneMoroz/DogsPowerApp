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
    /// A View Model For An Appointment
    /// </summary>
    public class AppointmentViewModel : BaseViewModel
    {
        #region Private Members

        private readonly IAppointmentEndpoint _appointmentEndpoint;

        #endregion

        #region Public Properties

        /// <summary>
        /// All services and options
        /// </summary>
        public List<ServiceModel> ServicesAndWeightOptions { get; set; }

        public List<WeightPriceDurationModel> OptionsDetails { get; set; }

        /// <summary>
        /// List of service names
        /// </summary>
        public ObservableCollection<string> Services { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// List of option names of selected service
        /// </summary>
        public ObservableCollection<string> Options { get; set; }

        /// <summary>
        /// Service choosen
        /// </summary>
        private string _selectedService;
        public string SelectedService
        {
            get
            {
                return _selectedService;
            }
            set
            {
                // Time options displayed not valid anymore, need to serach again with new inputs
                SearchIsDone = false;

                // Get options
                _selectedService = value;
                OptionsDetails = ((ServicesAndWeightOptions.Where(x => x.ServiceName == _selectedService)).FirstOrDefault()).Options;
                var output = new ObservableCollection<string>();
                foreach (var o in OptionsDetails)
                {
                    output.Add(o.WeightName);
                }

                Options = output;
                OnPropertyChanged(nameof(Options));
                OnPropertyChanged(nameof(SelectedOption));
            }
        }

        /// <summary>
        /// Option choosen
        /// </summary>
        private string _selectedOption;
        public string SelectedOption
        {
            get
            {
                return _selectedOption;
            }
            set
            {
                // Time options displayed not valid anymore, need to serach again with new inputs
                SearchIsDone = false;

                // Get service details(price & duration) taking into account weight choosen
                _selectedOption = value;
                var selectedOptionDetails = OptionsDetails.Where(x => x.WeightName == _selectedOption).FirstOrDefault();
                Price = selectedOptionDetails.Price;
                Duration = selectedOptionDetails.Duration;
                Weight = selectedOptionDetails.WeightName;

                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(Duration));
            }
        }

        /// <summary>
        /// Pets weight
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Chosen service price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Chosen service price display
        /// </summary>
        public string DisplayPrice => $"${Price}";

        /// <summary>
        /// Choosen service duration
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Chosen service duration display
        /// </summary>
        public string DisplayDuration
        {
            get
            {
                if(Duration.Minutes != 0)
                    return $"{Duration.Hours}hr {Duration.Minutes}min";
                else
                    return $"{Duration.Hours}hr";
            }
        }

        /// <summary>
        /// Selected date
        /// </summary>
        public DateTimeOffset SelectedDate
        {
            get
            {
                if (DisplayDate != null)
                {
                    string[] array = DisplayDate.Split("/");
                    return new DateTimeOffset(Int32.Parse("2021"), Int32.Parse(array[0]), Int32.Parse(array[1]), 0, 0, 0, TimeSpan.Zero);
                }
                else
                    return DateTimeOffset.Now.AddDays(1);
            }
        }

        /// <summary>
        /// Choosen displaydate
        /// </summary>
        private string _displayDate;
        public string DisplayDate
        {
            get
            {
                return _displayDate;
            }
            set
            {
                // Time options displayed not valid anymore, need to serach again with new inputs
                SearchIsDone = false;

                _displayDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        /// <summary>
        /// Indicates whether service/weight/date has been choosen and search time options is clicked
        /// If service or weight or date changed, then SearchIsReady becomes false => you need to click search again with new input
        /// </summary>
        public bool SearchIsDone { get; set; } = false;

        /// <summary>
        /// Time options
        /// </summary>
        private TimeOptionsListViewModel _timeOptions;
        public TimeOptionsListViewModel TimeOptions
        {
            get
            {
                return _timeOptions;
            }
            set
            {
                _timeOptions = value;
                OnPropertyChanged(nameof(TimeOptions));
            }
        }

        /// <summary>
        /// Choosen time
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Customer details
        /// </summary>
        public CustomerDetailsViewModel CustomerDetails { get; set; }

        #endregion

        #region Transactional Properties

        // Indicates that time options are loading
        public bool TimeOptionsAreLoading { get; set; }

        // Indicates that booking is taking place
        public bool BookingIsSaving { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Command to search time options given all inputs
        /// </summary>
        public ICommand SearchTimeOptionsCommand { get; set; }

        /// <summary>
        /// Book appointment
        /// </summary>
        public ICommand BookAppointmentCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppointmentViewModel(IAppointmentEndpoint appointmentEndpoint)
        {
            _appointmentEndpoint = appointmentEndpoint;

            // Create commands
            SearchTimeOptionsCommand = new RelayParameterizedCommand(async async => await SearchTimeOptionsAsync());
            BookAppointmentCommand = new RelayParameterizedCommand(async async => await BookAppointment());
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Search for available time options
        /// </summary>
        /// <returns></returns>
        public async Task SearchTimeOptionsAsync()
        {
            await RunCommandAsync(() => TimeOptionsAreLoading, async () =>
            {
                try
                {
                    // Load time options
                    var timeOptions = await GetTimeOptions(SelectedDate, Duration);

                    if (timeOptions.Count != 0)
                    {
                        _timeOptions = new TimeOptionsListViewModel { List = new List<TimeOptionsListItemViewModel>() };

                        // Add them to Time Option List Model
                        foreach (var option in timeOptions)
                        {
                            _timeOptions.List.Add(new TimeOptionsListItemViewModel
                            {
                                AvailableTime = option
                            });
                        }

                        OnPropertyChanged(nameof(TimeOptions));
                        SearchIsDone = true;
                    }
                    else
                    {
                        await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                        {
                            // TODO: Localize strings
                            Title = "No available time options",
                            Message = "No available time options, try to choose another date.",
                            OkText = "Ok",
                            NotOkText = "Back"
                        });
                    }
                    
                }
                catch (Exception ex)
                {
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Loading time options failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                }
            });
        }

        /// <summary>
        /// Book appointmant
        /// </summary>
        /// <returns></returns>
        public async Task BookAppointment()
        {
            await RunCommandAsync(() => BookingIsSaving, async () =>
            {
                try
                {

                }
                catch (Exception ex)
                {
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Booking failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });
                }
            });
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Load data when app starts
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync()
        {
            // Get all services and options from database
            ServicesAndWeightOptions = await _appointmentEndpoint.GetServicesAndWeightOptions();

            // Get a list of services
            foreach (var service in ServicesAndWeightOptions)
            {
                Services.Add(service.ServiceName);
            }

        }

        public async Task<List<TimeSpan>> GetTimeOptions(DateTimeOffset date, TimeSpan duration)
        {
            // Craete a list of available time options
            var availableTimeOptions = new List<TimeSpan>();

            // Call the database to get groomersId's and their available time options
            var groomersTimeOptions = await _appointmentEndpoint.GetAvailableTimeOptions(date, duration);

            // Order the list by the number of appoitments the groomers already has
            groomersTimeOptions.Sort((x, y) => x.NumberOfAppointments.CompareTo(y.NumberOfAppointments));

            // Go through the list and get rid of dublicate time options
            foreach (var groomer in groomersTimeOptions)
            {
                if (availableTimeOptions.Count != 0)
                {
                    var newOptions = groomer.AvailableTimeOptions.Where(x => !availableTimeOptions.Contains(x)).ToList();
                    availableTimeOptions.AddRange(newOptions);

                }
                else
                    availableTimeOptions.AddRange(groomer.AvailableTimeOptions);
            }

            return availableTimeOptions;
        }

        #endregion
    }
}

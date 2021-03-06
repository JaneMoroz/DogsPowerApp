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
                string[] array = DisplayDate.Split("/");
                // TODO: year will cause bugs when making appointment in december for january next year, need fix
                return new DateTimeOffset(int.Parse(DateTime.Now.Year.ToString()), int.Parse(array[0]), int.Parse(array[1]), 0, 0, 0, TimeSpan.Zero);
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
                if (_displayDate != null)
                {
                    return _displayDate;
                }
                else 
                    return DateTimeOffset.Now.ToString("MM'/'dd'/'yyyy");
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
        /// List of groomers with their available time options
        /// </summary>
        public List<GroomerTimeOptions> GroomersTimeOptions { get; set; }

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
        public CustomerDetailsViewModel CustomerDetails { get; set; } = new CustomerDetailsViewModel();

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

                        // Sort time options first
                        var orderedList = _timeOptions.List.OrderBy(x => x.AvailableTime.Hours).ThenBy(x => x.AvailableTime.Minutes).ToList();
                        _timeOptions.List = orderedList;

                        OnPropertyChanged(nameof(TimeOptions));
                        SearchIsDone = true;
                    }
                    else
                    {
                        SearchIsDone = true;
                        await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                        {
                            // TODO: Localize strings
                            Title = "No available time options",
                            Message = "No available time options, try to choose another date.",
                            OkText = "Ok",
                            NotOkText = "Back"
                        });
                        SearchIsDone = false;
                    }
                    
                }
                catch (Exception ex)
                {
                    SearchIsDone = true;
                    await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                    {
                        // TODO: Localize strings
                        Title = "Loading time options failed",
                        Message = ex.Message,
                        OkText = "Ok",
                        NotOkText = "Back"
                    });

                    SearchIsDone = false;
                }
            });
        }

        /// <summary>
        /// Book appointmant
        /// </summary>
        /// <returns></returns>
        public async Task BookAppointment()
        {
            // Appointment can be made not less than one hout an advance
            // TODO: Fix selectedDate giveing only year/momth/day => makes booean determination value impossible
            var timeLimit = DateTimeOffset.Now;
            bool time = SelectedDate >= timeLimit;
            await RunCommandAsync(() => BookingIsSaving, async () =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(CustomerDetails.FirstName) && !string.IsNullOrEmpty(CustomerDetails.LastName) 
                        && !string.IsNullOrEmpty(CustomerDetails.Phone) && !string.IsNullOrEmpty(CustomerDetails.Email)
                        && !string.IsNullOrEmpty(CustomerDetails.PetName) && time)
                    {
                        var groomerId = GroomersTimeOptions.Where(x => x.AvailableTimeOptions.Contains(Time)).FirstOrDefault().GroomerId;

                        await _appointmentEndpoint.CreateAppointment(new NewAppointmentDetails
                        {
                            FirstName = CustomerDetails.FirstName,
                            LastName = CustomerDetails.LastName,
                            Phone = CustomerDetails.Phone,
                            Email = CustomerDetails.Email,
                            PetName = CustomerDetails.PetName,
                            ServiceName = SelectedService,
                            WeightOption = SelectedOption,
                            Date = SelectedDate,
                            Time = Time,
                            GroomerId = groomerId
                        });

                        // Clear all fields
                        CustomerDetails.FirstName = "";
                        CustomerDetails.LastName = "";
                        CustomerDetails.Phone = "";
                        CustomerDetails.Email = "";
                        CustomerDetails.PetName = "";
                        SearchIsDone = false;
                    }
                    else
                    {
                        await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                        {
                            // TODO: Localize strings
                            Title = "Check details",
                            Message = "All fields must be provided, booking can't be made less than one hour in advance.",
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
            GroomersTimeOptions = await _appointmentEndpoint.GetAvailableTimeOptions(date, duration);

            // Order the list by the number of appoitments the groomers already has
            GroomersTimeOptions.Sort((x, y) => x.NumberOfAppointments.CompareTo(y.NumberOfAppointments));

            // Go through the list and get rid of dublicate time options
            foreach (var groomer in GroomersTimeOptions)
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

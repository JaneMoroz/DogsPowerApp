using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public class AppointmentData : IAppointmentData
    {
        #region Private Members

        private readonly ISqlDataAccess _sql;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public AppointmentData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        #endregion

        /// <summary>
        /// Get all service and their options
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceModel>> GetServicesAndWeightOptions()
        {
            // Get all service and options
            var servicesAndOptionDetails = await _sql.LoadData<ServicesAndWeightOptionsDbModel, dynamic>("dbo.spServices_GetServicesAndWeightOptions", new { }, "DPDataDb");

            var output = new List<ServiceModel>();

            // Go through each item and create ServiceModel => organize options into list
            foreach (var s in servicesAndOptionDetails)
            {
                // If a groomer doesn't exist, create new one
                if (output.FindIndex(x => x.ServiceName == s.ServiceName) == -1)
                {
                    var service = new ServiceModel
                    {
                        ServiceName = s.ServiceName,
                        Options = new List<WeightPriceDurationModel>()
                    };

                    service.Options.Add(new WeightPriceDurationModel
                    {
                        WeightName = s.WeightName,
                        Price = s.Price,
                        Duration = s.Duration
                    });
                    output.Add(service);
                }
                else
                {
                    // If service exists, just add option to list
                    var service = output.Find(x => x.ServiceName == s.ServiceName);
                    service.Options.Add(new WeightPriceDurationModel
                    {
                        WeightName = s.WeightName,
                        Price = s.Price,
                        Duration = s.Duration
                    });
                }
            }

            return output;
        }

        /// <summary>
        /// Get available time options
        /// </summary>
        /// <param name="date"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public async Task<List<GroomerTimeOptions>> GetAvailableTimeOptions(DateTimeOffset date, TimeSpan duration)
        {
            // Get weekday
            var weekday = date.DayOfWeek.ToString();
            // Get the list of available groomers Ids
            var availableGroomers = await _sql.LoadData<string, dynamic>("dbo.spWorkSchedule_FindAvailableGroomersByWorkday", new { Workday = weekday }, "DPDataDb");
            // Get all time options 
            var timeOptions = await _sql.LoadData<TimeSpan, dynamic>("dbo.spTimeOptions_GetAll", new { }, "DPDataDb");
            // List of all available time options across all groomers
            var availableTimeOptions = new List<GroomerTimeOptions>();

            // Go through each groomer
            foreach (var groomerId in availableGroomers)
            {
                // Get all his/her appointments for this day and get start time and duration for each
                var appointmentStartAndDuration = await _sql.LoadData<AppointmentDurationModel, dynamic>("dbo.spAppointments_GetByGroomerIdAndDate", new { GroomerId = groomerId, Date = date }, "DPDataDb");

                // List of time intervals occupied by appointments
                var notAvailableTime = new List<StartEndTime>();

                // List of available time intervals (service duration is taken into account)
                var availableTime = new List<StartEndTime>();

                // Workday starts at 9:00 and ends at 20:00
                var workHours = new StartEndTime
                {
                    StartTime = new DateTimeOffset(date.Year, date.Month, date.Day, 9, 00, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(date.Year, date.Month, date.Day, 20, 00, 0, TimeSpan.Zero)
                };

                if (appointmentStartAndDuration.Count != 0)
                {
                    //Populate the list of time intervals occupied by appointments
                    foreach (var a in appointmentStartAndDuration)
                    {
                        var startTime = new DateTimeOffset(date.Year, date.Month, date.Day, a.StartTime.Hours, a.StartTime.Minutes, 0, TimeSpan.Zero);
                        var StartEndTime = new StartEndTime
                        {
                            StartTime = startTime,
                            EndTime = startTime.Add(a.Duration)
                        };
                        notAvailableTime.Add(StartEndTime);
                    }

                    // Sort the list
                    notAvailableTime.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));

                    //  Populate the list of available time intervals

                    // Check 9:00 - ... interval
                    var firstAppointment = new StartEndTime { StartTime = workHours.StartTime, EndTime = workHours.StartTime.Add(duration) };
                    if (firstAppointment.EndTime < notAvailableTime[0].StartTime)
                        availableTime.Add(new StartEndTime() { StartTime = workHours.StartTime, EndTime = notAvailableTime[0].StartTime.Subtract(duration) });

                    // Check ... _ 20:00 interval
                    var lastAppointment = new StartEndTime { StartTime = workHours.EndTime.Subtract(duration), EndTime = workHours.EndTime };
                    if (lastAppointment.StartTime > notAvailableTime[notAvailableTime.Count - 1].EndTime)
                        availableTime.Add(new StartEndTime() { StartTime = notAvailableTime[notAvailableTime.Count - 1].EndTime, EndTime = workHours.EndTime.Subtract(duration) });

                    for (int i = 0; i <= notAvailableTime.Count - 1; i++)
                    {
                        if (i != notAvailableTime.Count - 1)
                            {
                                if (notAvailableTime[i].EndTime.Add(duration) < notAvailableTime[i + 1].StartTime)
                                    availableTime.Add(new StartEndTime() { StartTime = notAvailableTime[i].EndTime, EndTime = notAvailableTime[i + 1].EndTime.Subtract(duration) });
                            }
                    }
                }
                else
                {
                    // Get available time interval minus duration
                    availableTime.Add(new StartEndTime { StartTime = workHours.StartTime, EndTime = workHours.EndTime.Subtract(duration) });
                }

                // Check which time options are in the list of available time intervals
                if (availableTime.Count != 0)
                {
                    // Groomer available time options
                    var groomerAvailableTimeOptions = new GroomerTimeOptions()
                    {
                        GroomerId = groomerId,
                        NumberOfAppointments = appointmentStartAndDuration.Count,
                        AvailableTimeOptions = new List<TimeSpan>()
                    };

                    foreach (var timeOption in timeOptions)
                    {
                        var option = new DateTimeOffset(date.Year, date.Month, date.Day, timeOption.Hours, timeOption.Minutes, 0, TimeSpan.Zero);
                        foreach (var time in availableTime)
                        {
                            if (option >= time.StartTime && option <= time.EndTime)
                            {
                                // Add time options
                                groomerAvailableTimeOptions.AvailableTimeOptions.Add(timeOption);
                            }
                        }
                    }

                    // Add groomer with the list of time options to the list
                    availableTimeOptions.Add(groomerAvailableTimeOptions);
                } 
            }

            return availableTimeOptions;
        }

        /// <summary>
        /// Create new appointment
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task CreateAppointment(NewAppointmentDetails details)
        {
            // Create new customer if he/she doesn't exists
            DynamicParameters customerParameter = new DynamicParameters();

            customerParameter.Add("FirstName", details.FirstName);
            customerParameter.Add("LastName", details.LastName);
            customerParameter.Add("PhoneNumber", details.Phone);
            customerParameter.Add("Email", details.Email);

            var customerRecs = await _sql.LoadData<int, dynamic>("dbo.spCustomers_AddACustomer", customerParameter, "DPDataDb");

            // Get CustomerId
            var customerId = customerRecs.First();

            // Create new pet if it doesn't exist
            DynamicParameters petParameter = new DynamicParameters();

            petParameter.Add("CustomerId", customerId);
            petParameter.Add("PetName", details.PetName);
            petParameter.Add("WeightName", details.WeightOption);

            var petsRecs = await _sql.LoadData<int, dynamic>("dbo.spPets_AddAPet", petParameter, "DPDataDb");

            // Get PetId
            var petId = petsRecs.First();

            // Create new appointment
            DynamicParameters appointmentParameter = new DynamicParameters();

            appointmentParameter.Add("CustomerId", customerId);
            appointmentParameter.Add("GroomerId", details.GroomerId);
            appointmentParameter.Add("PetId", petId);
            appointmentParameter.Add("ServiceName", details.ServiceName);
            appointmentParameter.Add("WeightName", details.WeightOption);
            appointmentParameter.Add("Date", details.Date);
            appointmentParameter.Add("StartTime", details.Time);

            await _sql.SaveData("dbo.spAppointments_Add", appointmentParameter, "DPDataDb");
        }
    }
}

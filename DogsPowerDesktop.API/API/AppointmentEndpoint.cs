using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public class AppointmentEndpoint : IAppointmentEndpoint
    {
        #region Private Members

        private readonly IAPIHelper _apiHelper;

        #endregion

        #region Constructor
        public AppointmentEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #endregion

        /// <summary>
        /// Gets all service and their options
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceModel>> GetServicesAndWeightOptions()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Appointment/GetServicesAndWeightOptions"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ServiceModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Get available time options
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroomerTimeOptions>> GetAvailableTimeOptions(DateTimeOffset date, TimeSpan duration)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"api/Appointment/GetAvailableTimeOptions?date={date.ToString("yyyy-MM-dd")}&duration={duration}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<GroomerTimeOptions>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <returns></returns>
        public async Task CreateAppointment(NewAppointmentDetails details)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Appointment/CreateAppointment", details))
            {
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

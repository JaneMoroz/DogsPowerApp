using DogsPowerDataManager.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsPowerAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        #region Private Members

        private readonly IAppointmentData _appointmentData;

        #endregion

        #region Constructor

        public AppointmentController(IAppointmentData appointmentData)
        {
            _appointmentData = appointmentData;
        }

        #endregion

        #region New Appointment

        /// <summary>
        /// Gets all service and their options
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetServicesAndWeightOptions")]
        public async Task<List<ServiceModel>> GetAllGroomersAllDetails()
        {
            return await _appointmentData.GetServicesAndWeightOptions();
        }

        /// <summary>
        /// Get available time options
        /// </summary>
        /// <param name="date"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAvailableTimeOptions")]
        public async Task<List<GroomerTimeOptions>> GetAvailableTimeOptions(DateTimeOffset date, TimeSpan duration)
        {
            return await _appointmentData.GetAvailableTimeOptions(date, duration);
        }

        #endregion
    }
}

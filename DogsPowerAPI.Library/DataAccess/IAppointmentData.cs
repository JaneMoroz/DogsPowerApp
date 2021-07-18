using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public interface IAppointmentData
    {
        Task<List<ServiceModel>> GetServicesAndWeightOptions();
        Task<List<GroomerTimeOptions>> GetAvailableTimeOptions(DateTimeOffset date, TimeSpan duration);
    }
}
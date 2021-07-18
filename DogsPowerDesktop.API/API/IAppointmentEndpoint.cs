using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public interface IAppointmentEndpoint
    {
        Task<List<ServiceModel>> GetServicesAndWeightOptions();
        Task<List<GroomerTimeOptions>> GetAvailableTimeOptions(DateTimeOffset date, TimeSpan duration);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public interface IGroomersEndpoint
    {
        Task AddAGroomer(string id, string firstName, string lastName, string username, string email);
        Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails();
        Task<List<GroomerAppointmentsModel>> GetGroomerAppointments(string groomerId, DateTimeOffset date);
        Task<List<GroomerMinimumDetailsModel>> GetGroomersByWeekday(string weekday);
        Task UpdateWorkdays(string groomerId, List<string> groomerWorkdays);
        Task UploadPicture(string groomerId, byte[] picture);
        Task UpdateStatus(string groomerId, bool isActive);
    }
}
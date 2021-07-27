using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public interface IGroomersData
    {
        Task AddAGroomer(NewGroomerModel groomer);
        Task<List<GroomerDbModel>> GetAllGroomers();
        Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails();
        Task<List<GroomerMinimumDetailsModel>> GetGroomersByWeekday(string weekday);
        Task<List<GroomerAppointmentsModel>> GetGroomerAppointments(string groomerId, DateTimeOffset date);
        Task UpdateWorkdays(UpdateWorkdaysModel model);
        Task UploadPicture(UploadProfilePictureModel model);
        Task UpdateStatus(UpdateStatusModel model);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public interface IGroomersData
    {
        Task AddAGroomer(NewGroomerModel groomer);
        Task<List<GroomerDbModel>> GetAllGroomers();
        Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails();
        Task UpdateWorkdays(UpdateWorkdaysModel model);
    }
}
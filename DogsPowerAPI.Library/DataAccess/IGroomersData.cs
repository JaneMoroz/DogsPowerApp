using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public interface IGroomersData
    {
        Task<List<GroomerDbModel>> GetAllGroomers();
        Task AddAGroomer(NewGroomerModel groomer);

        Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails();
    }
}
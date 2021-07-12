using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public interface IGroomersData
    {
        Task<List<GroomerDbModel>> GetAllGroomers();
    }
}
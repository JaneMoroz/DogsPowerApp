using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public interface IAPIHelper
    {
        Task<ApiResponse<AuthenticatedUserModel>> Authenticate(string username, string password);
    }
}
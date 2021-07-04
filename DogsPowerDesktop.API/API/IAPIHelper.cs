using System.Net.Http;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<ApiResponse<AuthenticatedUserModel>> Authenticate(string username, string password);
    }
}
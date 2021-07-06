using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string roleName);
        Task<List<UserDetailsModel>> GetAll();
        Task<List<string>> GetAllRoles();
        Task RemoveUserFromRole(string userId, string roleName);
        Task<ApiResponse> Create(string username, string firstName, string lastName, string email, string password, string role);
    }
}
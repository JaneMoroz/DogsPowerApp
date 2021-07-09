using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogsPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        #region Private Members
        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The manager for handling roles management
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        public UserManagerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        #region User Manager

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ObservableCollection<UserDetailsModel>> GetAllUsers()
        {
            var output = new ObservableCollection<UserDetailsModel>();

            var users = await _userManager.Users.ToListAsync();

            foreach (var u in users)
            {
                var userRoles = await _userManager.GetRolesAsync(u);
                var user = new UserDetailsModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = userRoles.ToList()
                };

                output.Add(user);
            }

            return output;
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<List<string>> GetAllRoles()
        {
            List<string> output = new List<string>();

            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var r in roles)
            {
                output.Add(r.Name);
            }

            return output;
        }

        /// <summary>
        /// Add a role to a user
        /// </summary>
        /// <param name="pairing"> A model with userId and roleName</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRole")]
        public async Task AddRole(UserRolePairModel pairing)
        {
            // Find selected user by id
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            // Add selected role to user roles
            await _userManager.AddToRoleAsync(user, pairing.RoleName);

        }

        /// <summary>
        /// Remove a role from a user
        /// </summary>
        /// <param name="pairing"> A model with userId and roleName</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveRole")]
        public async Task DeleteRole(UserRolePairModel pairing)
        {
            // Find selected user by id
            var user = await _userManager.FindByIdAsync(pairing.UserId);

            // Remove selected role from user roles
            await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);

        }

        #endregion

        #region Create a new User

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<ApiResponse<UserDetailsModel>> Create([FromBody] RegisterCredentialsApiModel registerCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "The user with this username or email already exists";

            // The error response for a failed login
            var errorResponse = new ApiResponse<UserDetailsModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Check for the existence of username and email
            // if such user exists, return error message
            var usernameExists = await _userManager.FindByNameAsync(registerCredentials.Username);
            var emailExists = await _userManager.FindByEmailAsync(registerCredentials.Username);

            if (usernameExists != null)
                return errorResponse;
            else if (emailExists != null)
                return errorResponse;

            // Create the desired user from the given details
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerCredentials.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerCredentials.Username,
                FirstName = registerCredentials.FirstName,
                LastName = registerCredentials.LastName
            };

            // Try and create a user
            var result = await _userManager.CreateAsync(user, registerCredentials.Password);

            if (!result.Succeeded)
            {
                return new ApiResponse<UserDetailsModel>
                {
                    ErrorMessage = "User creation failed! Please check user details and try again."
                };
            }

            // Create a role if it doesn't already exist
            if (!await _roleManager.RoleExistsAsync(registerCredentials.Role))
                await _roleManager.CreateAsync(new IdentityRole(registerCredentials.Role));

            // Assign a choosen role to the user
            if(await _roleManager.RoleExistsAsync(registerCredentials.Role))
                result = await _userManager.AddToRoleAsync(user, registerCredentials.Role);

            if (!result.Succeeded)
            {
                return new ApiResponse<UserDetailsModel>
                {
                    ErrorMessage = "Couldn's assign a choosen role to a new user. Try to do it manually."
                };
            }

            // Find just created user by email
            var newUser = await _userManager.FindByEmailAsync(user.Email);
            var newUserRoles = await _userManager.GetRolesAsync(newUser);

            return new ApiResponse<UserDetailsModel>
            {
                Response = new UserDetailsModel
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Roles = newUserRoles.ToList()
                }
            };
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        public UserManagerController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        #endregion

        #region User Manager

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<UserDetailsModel>> GetAllUsers()
        {
            List<UserDetailsModel> output = new List<UserDetailsModel>();

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

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroomersController : ControllerBase
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
    }
}

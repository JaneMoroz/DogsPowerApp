using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
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
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        #endregion

        #region Login
        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <param name="loginCredentials">The login details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<ApiResponse<AuthenticatedUserModel>> Login([FromBody] LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<AuthenticatedUserModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a user name
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
            {
                // Return error message to user
                return errorResponse;
            }

            // Validate if the user credentials are correct...

            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await _userManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await _userManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If user exists and password is right
            if (user != null && await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
            {
                // Get user roles
                var userRoles = await _userManager.GetRolesAsync(user);

                // Return token to user
                return new ApiResponse<AuthenticatedUserModel>
                {
                    // Pass back the user details and the token
                    Response = new AuthenticatedUserModel
                    {
                        Access_Token = user.GenerateJwtToken(userRoles, _configuration),
                        UserName = user.UserName
                    }
                };

            }

            // In other cases return error
            return errorResponse;
        }
        #endregion

        #region Register

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCredentialsApiModel registerCredentials)
        {
            // Check for the existence of username and email
            // if such user exists, return error message
            var usernameExists = await _userManager.FindByNameAsync(registerCredentials.Username);
            var emailExists = await _userManager.FindByEmailAsync(registerCredentials.Username);

            if (usernameExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this username already exists!" });
            else if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this email already exists!" });

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

            if(!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary>
        /// Tries to register for an admin account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCredentialsApiModel registerCredentials)
        {
            // Check for the existence of username and email
            // if such user exists, return error message
            var usernameExists = await _userManager.FindByNameAsync(registerCredentials.Username);
            var emailExists = await _userManager.FindByEmailAsync(registerCredentials.Email);

            if (usernameExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this username already exists!" });
            else if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User with this email already exists!" });

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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            // Create roles if they don't already exist
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

            // Assign Admin role to the user
            if(await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        #endregion
    }
}

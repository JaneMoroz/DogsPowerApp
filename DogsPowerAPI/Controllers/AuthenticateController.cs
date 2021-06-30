using DogsPowerAPI.Library;
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
        public async Task<IActionResult> Login([FromBody] LoginCredentialsModel loginCredentials)
        {
            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await _userManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await _userManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                // Set our tokens claims
                var authClaims = new List<Claim>
                {
                    // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                    new Claim(ClaimTypes.Name, user.UserName),
                    // Unique ID for this token
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Get the secret key from configuration
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

                // Generate the Jwt Token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                    // Expire if not used for 9 hours
                    expires: DateTime.Now.AddHours(9)
                    );

                return Ok(new
                {
                    // Return the generated token
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
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
        public async Task<IActionResult> Register([FromBody] RegisterCredentialsModel registerCredentials)
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
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCredentialsModel registerCredentials)
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

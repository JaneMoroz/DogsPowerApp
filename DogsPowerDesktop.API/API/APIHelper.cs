using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DogsPowerDesktop.API
{
    public class APIHelper : IAPIHelper
    {
        // private readonly IConfiguration _config;
        private HttpClient _apiClient;

        public APIHelper()
        {
            // _config = config;
            InitializeClient();
        }

        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }
        private void InitializeClient()
        {

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri("https://localhost:44340/");
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Try to login user with given username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResponse<AuthenticatedUserModel>> Authenticate(string username, string password)
        {
            var loginCredentials = new LoginCredentialsModel
            {
                UsernameOrEmail = username,
                Password = password
            };

            using (HttpResponseMessage response = await _apiClient.PostAsJsonAsync("/api/authenticate/Login", loginCredentials))
            {
                var result = await response.Content.ReadAsAsync<ApiResponse<AuthenticatedUserModel>>();
                return result;
            }
        }
    }
}

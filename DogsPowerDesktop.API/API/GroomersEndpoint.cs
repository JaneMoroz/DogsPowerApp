using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerDesktop.API
{
    public class GroomersEndpoint : IGroomersEndpoint
    {
        #region Private Members

        private readonly IAPIHelper _apiHelper;

        #endregion

        #region Constructor
        public GroomersEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #endregion

        /// <summary>
        /// Create a new groomer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task AddAGroomer(string id, string firstName, string lastName, string username, string email)
        {
            var groomer = new NewGroomerModel
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email
            };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Groomers/AddAGroomer", groomer))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Get all groomers and details
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Groomers/GetAllGroomersAllDetails"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<GroomerDetailsModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

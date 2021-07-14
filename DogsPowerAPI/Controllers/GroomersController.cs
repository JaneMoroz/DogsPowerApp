using DogsPowerDataManager.Library;
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

        private readonly IGroomersData _groomersData;

        #endregion

        #region Constructor

        public GroomersController(IGroomersData groomersData)
        {
            _groomersData = groomersData;
        }

        #endregion

        #region Groomers

        /// <summary>
        /// Get all groomers from a groomers table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllGroomers")]
        public async Task<List<GroomerDbModel>> GetAllGroomers()
        {
            return await _groomersData.GetAllGroomers();
        }

        /// <summary>
        /// Get all groomers and their details from groomers/workshedule/profile pictures tables
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllGroomersAllDetails")]
        public async Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails()
        {
            return await _groomersData.GetAllGroomersAllDetails();
        }

        /// <summary>
        /// Get all groomers from a groomers table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAGroomer")]
        public async Task AddAGroomer(NewGroomerModel groomer)
        {
            await _groomersData.AddAGroomer(groomer);
        }

        /// <summary>
        /// Update a groomer's workdays
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateWorkdays")]
        public async Task UpdateWorkdays(UpdateWorkdaysModel model)
        {
            await _groomersData.UpdateWorkdays(model);
        }

        /// <summary>
        /// Upload Picture to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadPicture")]
        public async Task UploadPicture(UploadProfilePictureModel model)
        {
            await _groomersData.UploadPicture(model);
        }

        /// <summary>
        /// Upload Picture to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateStatus")]
        public async Task UpdateStatus(UpdateStatusModel model)
        {
            await _groomersData.UpdateStatus(model);
        }
        #endregion
    }
}

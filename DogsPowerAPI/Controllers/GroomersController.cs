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

        [HttpGet]
        [Route("GetAllGroomers")]
        public async Task<List<GroomerDbModel>> GetAllGroomers()
        {
            return await _groomersData.GetAllGroomers();

        }

        #endregion
    }
}

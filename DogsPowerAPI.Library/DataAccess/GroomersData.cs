using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public class GroomersData : IGroomersData
    {
        #region Private Members

        private readonly ISqlDataAccess _sql;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public GroomersData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        #endregion

        /// <summary>
        /// Get all groomers from groomers table only
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<List<GroomerDbModel>> GetAllGroomers()
        {
            return await _sql.LoadData<GroomerDbModel, dynamic>("dbo.spGroomers_GetAllGroomers", new { }, "DPDataDb");
        }

        /// <summary>
        /// Get all groomers and their details from groomers/workshedule/profile pictures tables
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<List<GroomerDetailsModel>> GetAllGroomersAllDetails()
        {
            // Get groomers, all their details
            var groomers = await _sql.LoadData<GroomersDetailsDbModel, dynamic>("dbo.spGroomers_GetAllGroomersAllDetails", new { }, "DPDataDb");

            var output = new List<GroomerDetailsModel>();

            // Go through each groomer and create list of workdays for each
            foreach (var g in groomers)
            {
                // If a groomer doesn't exist, create new one
                if (output.FindIndex(x => x.Id == g.Id) == -1)
                {
                    var groomer = new GroomerDetailsModel
                    {
                        Id = g.Id,
                        Username = g.Username,
                        FirstName = g.FirstName,
                        LastName = g.LastName,
                        Email = g.Email,
                        IsActive = g.IsActive,
                        ProfilePicture = g.Picture,
                        Workdays = new List<string>()
                    };
                    if (g.Workday != null)
                    {
                        groomer.Workdays.Add(g.Workday);
                    }
                    output.Add(groomer);
                }
                else
                {
                    // If he/she exists, just add workdays to his/het list of workdays
                    var groomer = output.Find(x => x.Id == g.Id);
                    groomer.Workdays.Add(g.Workday);
                }
            }

            return output;
        }

        /// <summary>
        /// Add a new groomer to a Groomers table
        /// </summary>
        /// <returns></returns>
        public Task AddAGroomer(NewGroomerModel groomer)
        {
            return _sql.SaveData("dbo.spGroomers_AddAGroomer", groomer, "DPDataDb");
        }

        /// <summary>
        /// Update a groomer's workdays
        /// </summary>
        /// <returns></returns>
        public async Task UpdateWorkdays(UpdateWorkdaysModel model)
        {
            // Get currently saved workdays from WorkSchedule by groomer Id
            var workdays = await _sql.LoadData<string, dynamic>("dbo.spWorkSchedule_GetById", new { GroomerId = model.GroomerId }, "DPDataDb");

            // Compare current list of workdays with changed one, add/remove workdays to/from current list of workdays
            foreach (var gw in model.GroomerWorkdays)
            {
                if (!workdays.Contains(gw))
                {
                    await _sql.SaveData("dbo.spWorkSchedule_Add", new { GroomerId = model.GroomerId, Workday = gw }, "DPDataDb");
                }
            }

            var removedWorkdays = workdays.Except(model.GroomerWorkdays).ToList();

            foreach (var rw in removedWorkdays)
            {
                await _sql.SaveData("dbo.spWorkSchedule_Remove", new { GroomerId = model.GroomerId, Workday = rw }, "DPDataDb");
            }
        }

        /// <summary>
        /// Upload profile picture to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UploadPicture(UploadProfilePictureModel model)
        {
            // Check wether the groomer has a profile picture already
            var groomerHasPicture = await _sql.LoadData<int, dynamic>("dbo.spProfilePictures_GetByGroomerId", new { GroomerId = model.GroomerId }, "DPDataDb");

            // If he/she has
            if(groomerHasPicture.Count != 0)
            {
                // Delete current picture
                await _sql.SaveData("dbo.spProfilePictures_Delete", new { GroomerId = model.GroomerId }, "DPDataDb");
            }

            // Add new picture
            await _sql.SaveData("dbo.spProfilePictures_Add", new { GroomerId = model.GroomerId, Picture = model.Picture }, "DPDataDb");
        }

        /// <summary>
        /// Update groomers's status
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateStatus(UpdateStatusModel model)
        {
            // Update status
            await _sql.SaveData("dbo.spGroomers_UpdateStatus", new { Id = model.GroomerId, IsActive = model.IsActive }, "DPDataDb");
        }
    }
}

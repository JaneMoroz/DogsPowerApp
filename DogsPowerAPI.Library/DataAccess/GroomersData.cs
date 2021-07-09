using System;
using System.Collections.Generic;
using System.Text;

namespace DogsPowerDataManager.Library
{
    public class GroomersData
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
        /// Get all groomers with the list of theit workdays
        /// </summary>
        /// <returns></returns>
        public List<GroomerDetailsModel> GetAllGroomers()
        {
            // Get all groomers
            var listOfGroomers = _sql.LoadData<GroomerDetailsDbModel, dynamic>("dbo.spGroomers_GetAll", new { }, "DPDataDb");

            var output = new List<GroomerDetailsModel>();

            // Go through each groomer and create list of workdays fro each
            foreach (var g in listOfGroomers)
            {
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
                        Workdays = new List<string>()
                    };

                    groomer.Workdays.Add(g.Workday);
                    output.Add(groomer);
                }
                else
                {
                    var groomer = output.Find(x => x.Id == g.Id);
                    groomer.Workdays.Add(g.Workday);
                }  
            }

            return output;
        }
    }
}

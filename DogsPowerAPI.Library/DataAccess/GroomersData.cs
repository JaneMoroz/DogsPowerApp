using System;
using System.Collections.Generic;
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
        public Task<List<GroomerDbModel>> GetAllGroomers()
        {
            return _sql.LoadData<GroomerDbModel, dynamic>("dbo.spGroomers_GetAllGroomers", new { }, "DPDataDb");
        }
    }
}

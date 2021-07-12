using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsPowerDataManager.Library
{
    public class SqlDataAccess : ISqlDataAccess
    {
        #region Private Properties

        private readonly IConfiguration _config;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        /// <summary>
        /// Load data from Db
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="storedProcedure">Stored procedure</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="connectionStringName">The name of Db</param>
        /// <returns></returns>
        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(storedProcedure,
                                                   parameters,
                                                   commandType: CommandType.StoredProcedure);
                return rows.ToList();
            }
        }

        /// <summary>
        /// Save data to db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">Stored Procedure</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="connectionStringName">The name of Db</param>
        /// <returns></returns>
        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #region Private helpers

        /// <summary>
        /// Shortcut to get a connection string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        #endregion
    }
}

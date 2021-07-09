using System.Collections.Generic;

namespace DogsPowerDataManager.Library
{
    public interface ISqlDataAccess
    {
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}
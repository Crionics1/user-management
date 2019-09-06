using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;

namespace UserManagement.Repository
{
    public class DataManager<T> : IDataManager<T> where T : class
    {
        protected IDbConnection _dbConnection;
        public DataManager(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public T Create(T t)
        {
            try
            {
                _dbConnection.Open();
                _dbConnection.Insert<T>(t);
            }
            finally
            {
                _dbConnection.Close();
            }
            return t;
        }

        public virtual T Delete(T t)
        {
            try
            {
                _dbConnection.Open();
                _dbConnection.Delete<T>(t);
            }
            finally
            {
                _dbConnection.Close();
            }
            return t;
        }

        public T Get(int id)
        {
            T t;
            try
            {
                _dbConnection.Open();
                t = _dbConnection.Get<T>(id);
            }
            finally
            {
                _dbConnection.Close();
            }
            return t;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> enumerable;
            try
            {
                _dbConnection.Open();
               enumerable = _dbConnection.GetAll<T>();
            }
            finally
            {
                _dbConnection.Close();
            }
            return enumerable;
        }

        public T Update(T t)
        {
            try
            {
                _dbConnection.Open();
                _dbConnection.Update<T>(t);
            }
            finally
            {
                _dbConnection.Close();
            }
            return t;
        }
    }
}

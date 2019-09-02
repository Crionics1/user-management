using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace UserManagement.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        private IDbConnection _dbConnection;
        public virtual string CreateProcedureName => $"Create{typeof(T).Name}";
        public virtual string UpdateProcedureName => $"Update{typeof(T).Name}";
        public virtual string DeleteProcedureName => $"Delete{typeof(T).Name}";
        public virtual string GetAllProcedureName => $"GetAll{typeof(T).Name}";
        public virtual string GetByIDProcedureName => $"GetByID{typeof(T).Name}";

        public BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public virtual int Create(SqlMapper.IDynamicParameters dynamicParameters)
        {
            return Action(CreateProcedureName, dynamicParameters, CommandType.StoredProcedure);
        }

        public virtual int Delete(SqlMapper.IDynamicParameters dynamicParameters)
        {
            return Action(DeleteProcedureName, dynamicParameters, CommandType.StoredProcedure);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbConnection.Query<T>(GetAllProcedureName,commandType: CommandType.StoredProcedure).AsList<T>();
        }

        public virtual T GetByID(int id)
        {
            return _dbConnection.QueryFirstOrDefault<T>(GetByIDProcedureName,new SqlParameter[] { new SqlParameter("Id",id)}, commandType: CommandType.StoredProcedure);
        }

        public virtual int Update(SqlMapper.IDynamicParameters dynamicParameters)
        {
            return Action(UpdateProcedureName, dynamicParameters, CommandType.StoredProcedure);
        }

        protected virtual int Action(string procedureName, SqlMapper.IDynamicParameters dynamicParameters, CommandType commandType)
        {
            int result;
            try
            {
                _dbConnection.Open();
                result = _dbConnection.Execute(procedureName, dynamicParameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                _dbConnection.Close();
            }
            return result;
        }
    }
}

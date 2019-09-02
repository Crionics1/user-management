using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UserManagement.Domain.Interfaces
{
    public interface IRepository<T>
    {
        int Create(SqlMapper.IDynamicParameters dynamicParameters);
        int Update(SqlMapper.IDynamicParameters dynamicParameters);
        int Delete(SqlMapper.IDynamicParameters dynamicParameters);
        T GetByID(int id);
        IEnumerable<T> GetAll();
    }
}

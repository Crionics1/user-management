using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserManagement.Domain.Entities;

namespace UserManagement.Repository
{
    public class UserRepository: BaseRepository<User>
    {
        public UserRepository(IDbConnection dbConnection) : base(dbConnection){ }
    }
}

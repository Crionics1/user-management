using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserManagement.Domain.Entities;
using Dapper.Contrib.Extensions;
using System.Transactions;
using System.Linq;

namespace UserManagement.Repository
{
    public class UserDataManager : DataManager<User>, IUserDataManager
    {
        public UserDataManager(IDbConnection dbConnection) : base(dbConnection) { }

        public new User Delete(User user)
        {
            using (var scope = new TransactionScope())
            {
                _dbConnection.Open();
                var addresses = _dbConnection.GetAll<Address>().Where(a => a.UserID == user.ID);

                foreach (var address in addresses)
                {
                    _dbConnection.Delete<Address>(address);
                }

                _dbConnection.Delete<User>(user);
                scope.Complete();

                return user;
            }
        }
    }
}

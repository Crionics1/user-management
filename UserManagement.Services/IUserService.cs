using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities;

namespace UserManagement.Services
{
    public interface IUserService : IService<User>
    {
        User GetFull(int id);
        IEnumerable<User> GetByFirstName(string name);
        IEnumerable<User> GetByLastName(string name);
        User GetByMobile(string mobile);
        User GetByEmail(string email);
        User GetByPrivateID(string privateID);
    }
}

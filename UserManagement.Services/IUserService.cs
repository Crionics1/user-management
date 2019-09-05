using System.Collections.Generic;
using UserManagement.Domain.Entities;

namespace UserManagement.Services
{
    public interface IUserService : IService<User>
    {
        IEnumerable<User> GetByFirstName(string name);
        IEnumerable<User> GetByLastName(string name);
        User GetByMobile(string mobile);
        User GetByEmail(string email);
        User GetByPrivateID(string privateID);
    }
}

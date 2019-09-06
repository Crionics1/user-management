using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Entities;

namespace UserManagement.Repository
{
    public interface IUserDataManager : IDataManager<User>
    {
    }
}

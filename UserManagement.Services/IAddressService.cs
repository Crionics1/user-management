﻿using System.Collections.Generic;
using UserManagement.Domain.Entities;

namespace UserManagement.Services
{
    public interface IAddressService : IService<Address>
    {
        int GetUserID(int id);
        IEnumerable<Address> GetByUser(User user);
        IEnumerable<Address> GetByUserPrivateID(string privateId);
    }
}

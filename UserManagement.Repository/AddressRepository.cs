using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UserManagement.Domain.Entities;

namespace UserManagement.Repository
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(IDbConnection dbConnection) : base(dbConnection) { }
    }
}

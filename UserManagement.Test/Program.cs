using System;
using System.Data.SqlClient;
using UserManagement.Domain.Entities;
using UserManagement.Repository;
using UserManagement.Services;

namespace UserManagement.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection("Server = DESKTOP-1664Q4O ; Database = UserManagement; Integrated Security = True;");
            UserDataManager userDataManager = new UserDataManager(sqlConnection);
            DataManager<Address> addressManager = new DataManager<Address>(sqlConnection);

            AddressService addressService = new AddressService(addressManager,userDataManager);
            UserService userService = new UserService(userDataManager);

            userService.Delete(25);


        }
    }
}

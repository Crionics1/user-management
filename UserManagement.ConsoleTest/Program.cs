using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using UserManagement.Domain.Helpers;
using UserManagement.Domain.Entities;
using UserManagement.Repository;
using UserManagement.Services;

namespace UserManagement.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.FirstName = "Luka";
            user.LastName = "Turman";
            user.Mobile = "9955522355";
            user.PrivateID = "61001071121";
            user.Resident = "Georgia";
            user.RegistrationDate = DateTime.Now;
            user.DateOfBirth = DateTime.Now;
            user.Email = "lukat321m20@gmail.com";
            user.Gender = true;
            user.Language = "Georgian";
            user.Password = "Luka1333321";
            user.RegistrationIP = "192.168.100.156";

            Address address = new Address();
            address.Country = "Georgia";
            address.City = "Batumi";
            address.Region = "Adjara";
            address.PostalCode = "06665";
            address.Address1 = "street chavchavadze";
            address.UserID = 1;

            SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-EHGAQ47\\LUKASQL; Database=UserManagement; Integrated Security=True;");
            DataManager<User> dataManager = new DataManager<User>(sqlConnection);
            DataManager<Address> dataManager1 = new DataManager<Address>(sqlConnection);

            AddressService addressService = new AddressService(dataManager1,dataManager);
            UserService userService = new UserService(dataManager,addressService);

            userService.Create(user);

            Console.ReadKey();
        }
    }

}

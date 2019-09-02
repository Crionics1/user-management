using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using UserManagement.Domain.Attributes;
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
            user.ID = 8;
            user.FirstName = "Luka";
            user.LastName = "Turman";
            user.Mobile = "9955555555";
            user.PrivateID = "61001071111";
            user.Resident = "Georgia";
            user.RegistrationDate = DateTime.Now;
            user.DateOfBirth = DateTime.Now;
            user.Email = "lukaturm20@gmail.com";
            user.Gender = true;
            user.Language = "Georgian";
            user.Password = "Luka123321";
            user.RegistrationIP = "192.168.100.156";

            SqlConnection sqlConnection = new SqlConnection("Server=localhost; Database=UserManagement; Integrated Security=True;");
            DataManager<User> dataManager = new DataManager<User>(sqlConnection);
            DataManager<Address> dataManager1 = new DataManager<Address>(sqlConnection);

            AddressService addressService = new AddressService(dataManager1,dataManager);
            UserService userService = new UserService(dataManager,addressService);

            var rame = dataManager.Get(2);
            Console.WriteLine(rame);

            Console.ReadKey();
        }
    }

}

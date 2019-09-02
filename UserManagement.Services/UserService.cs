using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UserManagement.Domain.Entities;
using UserManagement.Repository;
using System.Linq;
using UserManagement.Domain.Enums;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private IDataManager<User> _dataManager;
        private IAddressService _addresService;

        public UserService(IDataManager<User> dataManager, IAddressService addresService)
        {
            _dataManager = dataManager;
            _addresService = addresService;
        }

        public User Create(User t)
        {
            ValidateMobile(t);
            ValidatePrivateID(t);

            try
            {
                GetByEmail(t.Email);
                GetByMobile(t.Mobile);
            }
            catch (Exception)
            {
                string hashedPassword = Hash(t.Password);
                t.Password = hashedPassword;

                return _dataManager.Create(t);
            }

            throw new Exception(message: "Can not create user. Email or Mobile number already in use!");
        }

        public User Update(User t)
        {
            ValidateMobile(t);
            ValidatePrivateID(t);

            Get(t.ID);

            try
            {
                GetByEmail(t.Email);
                GetByMobile(t.Mobile);
            }
            catch (Exception)
            {
                return _dataManager.Update(t);
            }

            throw new Exception(message: "Can not update user. Email or Mobile number already in use!");
        }

        public User Delete(User t)
        {
            Get(t.ID);

            return _dataManager.Delete(t);
        }

        public User Get(int id)
        {
            var user = _dataManager.Get(id);
            if (user == null)
            {
                throw new Exception(message: "Such user does not exist");
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _dataManager.GetAll();
        }

        public User GetByEmail(string email)
        {
            User user;
            try
            {
                user = _dataManager.GetAll().Single(u => u.Email == email);
            }
            catch (Exception)
            {
                throw new Exception(message: "There's no user associated with such Email.");
            }
            return user;
        }

        public User GetByMobile(string mobile)
        {
            User user;
            try
            {
                user = _dataManager.GetAll().Single(u => u.Mobile == mobile);
            }
            catch (Exception)
            {
                throw new Exception(message: "There's no user associated with such mobile number.");
            }
            return user;
        }

        public IEnumerable<User> GetByFirstName(string firstName)
        {
            return _dataManager.GetAll().Where(u => u.FirstName.StartsWith(firstName));
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            return _dataManager.GetAll().Where(u => u.LastName.StartsWith(lastName));
        }

        public User GetByPrivateID(string privateID)
        {
            User user;
            try
            {
                user = _dataManager.GetAll().Single(u => u.PrivateID == privateID);
            }
            catch (Exception)
            {
                throw new Exception(message: "There's no user associated with such private ID.");
            }
            return user;
        }
        public User GetFull(int id)
        {
            var user = Get(id);
            var address = _addresService.GetByUser(user);

            user.Addresses = address;
            return user;
        }

        #region Validations

        private bool ValidateMobile(User user)
        {
            MobilePrefixes mobilePrefix = (MobilePrefixes)Enum.Parse(typeof(MobilePrefixes), user.Resident);
            int value = (int)mobilePrefix;

            if (!user.Mobile.StartsWith(value.ToString()))
            {
                throw new Exception(message: "Mobile prefix should match users country mobile prefix!");
            }
            return true;
        }

        private bool ValidatePrivateID(User user)
        {
            PrivateIDLengths privateIDLength = (PrivateIDLengths)Enum.Parse(typeof(PrivateIDLengths), user.Resident);
            if (user.PrivateID.Length != (int)privateIDLength)
            {
                throw new Exception(message: "Private ID should match users country private ID length!");
            }
            return true;
        }

        #endregion

        private string Hash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

    }
}

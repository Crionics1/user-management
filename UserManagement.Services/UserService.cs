﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UserManagement.Domain.Entities;
using UserManagement.Repository;
using System.Linq;
using UserManagement.Domain.Enums;
using UserManagement.Domain.Helpers;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private IUserDataManager _dataManager;

        public UserService(IUserDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public User Create(User user)
        {
            Validate(user);
            
            string hashedPassword = Hash(user.Password);
            user.Password = hashedPassword;

            return _dataManager.Create(user);
        }

        public User Update(User user)
        {
            Get(user.ID);
            Validate(user);

            return _dataManager.Update(user);
        }

        public bool Delete(int id)
        {
            var user = Get(id);

            _dataManager.Delete(user);

            return true;
        }

        public User Get(int id)
        {
            var user = _dataManager.Get(id);
            if (user == null)
            {
                throw new NotFoundException(message: "Such user does not exist");
            }


            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = _dataManager.GetAll();

            return users;
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
                throw new NotFoundException(message: "There's no user associated with such Email.");
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
                throw new NotFoundException(message: "There's no user associated with such mobile number.");
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
                throw new NotFoundException(message: "There's no user associated with such private ID.");
            }
            return user;
        }

        #region Validations

        private bool ValidateMobile(User user)
        {
            MobilePrefixes mobilePrefix = (MobilePrefixes)Enum.Parse(typeof(MobilePrefixes), user.Resident);
            int value = (int)mobilePrefix;

            if (!user.Mobile.StartsWith(value.ToString()))
            {
                throw new BadRequestException(message: "Mobile prefix should match users country mobile prefix!");
            }

            return true;
        }

        private bool ValidatePrivateID(User user)
        {
            PrivateIDLengths privateIDLength = (PrivateIDLengths)Enum.Parse(typeof(PrivateIDLengths), user.Resident);
            if (user.PrivateID.Length != (int)privateIDLength)
            {
                throw new BadRequestException(message: "Private ID should match users country private ID length!");
            }
            return true;
        }

        private bool ValidateUniqueKeys(User user)
        {
            List<User> users = (List<User>)GetAll();
            users.Remove(users.FirstOrDefault(u => u.ID == user.ID));

            if (users.FirstOrDefault(u => u.Mobile == user.Mobile) == null &&
                users.FirstOrDefault(u => u.PrivateID == user.PrivateID) == null &&
                users.FirstOrDefault(u => u.Email == user.Email) == null)
            {
                return true;
            }
            throw new BadRequestException("Can not create user. Email, Mobile number or Private ID is already in use!");
        }

        private bool Validate(User user)
        {
            ValidateMobile(user);
            ValidatePrivateID(user);
            ValidateUniqueKeys(user);

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

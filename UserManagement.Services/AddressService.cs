using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Helpers;
using UserManagement.Repository;

namespace UserManagement.Services
{
    public class AddressService : IAddressService
    {
        private IDataManager<Address> _dataManager;
        private IDataManager<User> _userDataManager;

        public AddressService(IDataManager<Address> dataManager,IDataManager<User> userDataManager)
        {
            _userDataManager = userDataManager;    
            _dataManager = dataManager;
        }

        public Address Create(Address t)
        {
            var user = _userDataManager.Get(t.UserID);
            if (user == null)
            {
                throw new BadRequestException("No such user exists!");
            }

            return _dataManager.Create(t);
        }

        public bool Delete(int id)
        {
            var adr = Get(id);
            _dataManager.Delete(adr);

            return true;
        }

        public Address Get(int id)
        {
            var address = _dataManager.Get(id);
            if (address==null)
            {
                throw new NotFoundException(message: "Address does not exist");
            }
            return address;
        }

        public IEnumerable<Address> GetAll()
        {
            return _dataManager.GetAll();
        }

        public Address Update(Address t)
        {
            Get(t.ID);
            //var user = _userDataManager.Get(t.UserID);
            //if (user == null)
            //{
            //    throw new NotFoundException("No such user exists!");
            //}

            return _dataManager.Update(t);
        }

        public int GetUserID(int id)
        {
            var address = Get(id);
            return address.UserID;
        }

        public IEnumerable<Address> GetByUser(User user)
        {
            return GetAll().Where(a => a.UserID == user.ID); 
        }

        public IEnumerable<Address> GetByUserPrivateID(string privateId)
        {
            User user;
            try
            {
                user = _userDataManager.GetAll().Single(u => u.PrivateID == privateId);
            }
            catch (Exception)
            {
                throw new NotFoundException(message: "User with such Private ID does not exist!");
            }

            var addresses = _dataManager.GetAll().Where(a => a.UserID == user.ID);

            return addresses;
        }
    }
}

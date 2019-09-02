using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UserManagement.Domain.Entities;
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
                throw new Exception("No such user exists!");
            }

            return _dataManager.Create(t);
        }

        public Address Delete(Address t)
        {
            Get(t.ID);
            
            return _dataManager.Delete(t);
        }

        public Address Get(int id)
        {
            var address = _dataManager.Get(id);
            if (address==null)
            {
                throw new Exception(message: "Address does not exist");
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
            var user = _userDataManager.Get(t.UserID);
            if (user == null)
            {
                throw new Exception("No such user exists!");
            }

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

    }
}

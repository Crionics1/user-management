using System.Collections.Generic;

namespace UserManagement.Services
{
    public interface IService<T> where T : class
    {
        T Create(T t);
        T Update(T t);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}

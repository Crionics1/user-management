using System.Collections.Generic;

namespace UserManagement.Repository
{
    public interface IDataManager<T> where T : class
    {
        T Create(T t);
        T Update(T t);
        T Delete(T t);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}

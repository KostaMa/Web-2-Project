using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectPerson.DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByObject(object obj);
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(T obj);
        Task Delete(int id);
        Task Save();
    }
}

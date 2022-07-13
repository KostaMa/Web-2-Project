using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectPerson.Common.Models;

namespace ProjectPerson.DataAccess.IRepository
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonByEmail(string email);
        Task<IEnumerable<Person>> GetPersonByType(Enums.PersonType type);
        Task<Person> GetPersonByUserId(int id);
    }
}

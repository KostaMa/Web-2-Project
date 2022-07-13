using Microsoft.EntityFrameworkCore;
using ProjectPerson.Common.Models;
using ProjectPerson.DataAccess;
using ProjectPerson.DataAccess.IRepository;
using ProjectPerson.DataAccess.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Project.DataAccess.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataAccessContext context) : base(context) { }

        public async Task<User> GetUserByUsername(string username)
        {
            return await table.Where(u => u.UserName == username)?.FirstOrDefaultAsync();
        }
    }
}

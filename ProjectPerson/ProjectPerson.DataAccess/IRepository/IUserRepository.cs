using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectPerson.Common.Models;

namespace ProjectPerson.DataAccess.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
    }
}

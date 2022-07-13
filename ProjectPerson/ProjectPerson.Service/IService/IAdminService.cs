using System;
using System.Threading.Tasks;

namespace ProjectPerson.Service.IService
{
    public interface IAdminService
    {
        Task AcceptAccount(int idPerson);
        Task DeniedAccount(int idPerson);
    }
}

using System;
using System.Threading.Tasks;
using ProjectPerson.Common.DTOs;
using ProjectPerson.Common.Models;

namespace ProjectPerson.Service.IService
{
    public interface IAccountService
    {
        Task<bool> Regeister(RegisterDTO dto, Enums.PersonType type);
        Task<PersonDTO> GetPerson(string email);
    }
}

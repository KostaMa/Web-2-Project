using System;
using System.Threading.Tasks;
using ProjectPerson.Common.DTOs;
using ProjectPerson.Common.Models;
using ProjectPerson.WebApi.DTOs;

namespace ProjectPerson.WebApi.Authentication
{
    public interface IAuthenticateService
    {
        Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model);
        Task<User> GetById(int id);
    }
}

using Microsoft.AspNetCore.Http;
using ProjectPerson.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPerson.Service.IService
{
    public interface IPersonService
    {
        Task<PersonDTO> GetPersonById(int id);
        Task<PersonDTO> GetPersonByEmail(string email);
        Task<IEnumerable<PersonDTO>> GetDeliverer();
        Task<IEnumerable<PersonDTO>> GetAll();
        Task<bool> UpdatePerson(ProfileDTO dto);
        Task<bool> DeletePerson(int idPerson);
        Task UploadImage(int idPerson, IFormFile file);
        Task<string> GetImage(int idPerson);
    }
}

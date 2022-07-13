using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProjectPerson.Common.DTOs;
using ProjectPerson.Common.Models;
using ProjectPerson.DataAccess.IRepository;
using ProjectPerson.Service.IService;

namespace ProjectPerson.Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _genericUserRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Person> _genericPersonRepository;
        private readonly IMapper _mapper;

        public AccountService(IGenericRepository<User> genericUserRepository, IUserRepository userRepository, IPersonRepository personRepository, IGenericRepository<Person> genericPersonRepository, IMapper mapper)
        {
            _genericUserRepository = genericUserRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _genericPersonRepository = genericPersonRepository;
            _mapper = mapper;
        }

        public async Task<bool> Regeister(RegisterDTO dto, Enums.PersonType type)
        {
            User user = await _userRepository.GetUserByUsername(dto.UserName);
            if (user != null)
            {
                return true;
            }

            user = new User
            {
                UserName = dto.UserName,
            };

            if (!String.IsNullOrEmpty(dto.Password))
            {
                ProjectPerson.Service.PasswordHasher.PasswordHasher hasher = new ProjectPerson.Service.PasswordHasher.PasswordHasher();
                user.Password = hasher.Hash(dto.Password);
            }

            await _genericUserRepository.Insert(user);
            await _genericUserRepository.Save();
            user = await _userRepository.GetUserByUsername(dto.UserName);

            Person person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Birth = Convert.ToDateTime(dto.Birth),
                Address = dto.Address,
                IdUser = user.Id,
                ImageUrl = dto.ImageUrl
            };

            if (type == Enums.PersonType.Customer)
            {
                person.PersonType = type;
                person.Verification = Enums.VerificationStatus.None;
            }
            else if (type == Enums.PersonType.Deliverer)
            {
                person.PersonType = type;
                person.Verification = Enums.VerificationStatus.OnHold;
            }

            await _genericPersonRepository.Insert(person);
            await _genericPersonRepository.Save();

            return false;
        }

        public async Task<PersonDTO> GetPerson(string email)
        {
            Person person = await _personRepository.GetPersonByEmail(email);
            User user = await _genericUserRepository.GetByObject(person.IdUser);

            if (person == null || user == null)
            {
                throw new KeyNotFoundException("User does not exists.");
            }

            PersonDTO dto = _mapper.Map<PersonDTO>(person);
            dto.UserName = user.UserName;

            return dto;
        }
    }
}

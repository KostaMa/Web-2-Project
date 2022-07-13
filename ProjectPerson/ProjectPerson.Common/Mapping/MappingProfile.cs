using System;
using System.Collections.Generic;
using AutoMapper;
using ProjectPerson.Common.DTOs;
using ProjectPerson.Common.Models;

namespace ProjectPerson.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<User, AuthenticateRequestDTO>().ReverseMap();
        }
    }
}

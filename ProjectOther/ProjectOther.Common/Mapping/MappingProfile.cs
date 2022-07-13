using System;
using System.Collections.Generic;
using AutoMapper;
using ProjectOther.Common.DTOs;
using ProjectOther.Common.Models;

namespace ProjectOther.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}

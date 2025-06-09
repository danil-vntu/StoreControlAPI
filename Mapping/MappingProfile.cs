﻿using AutoMapper;
using StoreControlAPI.DTOs;
using StoreControlAPI.Models;

namespace StoreControlAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            //CreateMap<Product, ProductDto>();
            CreateMap<User, UserDto>();
        }
    }
}

﻿using AutoMapper;
using VillaAPI.Models;
using VillaAPI.Models.DTO;

namespace VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Villa , VillaDTO>().ReverseMap();

            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();

            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

        }
    }
}

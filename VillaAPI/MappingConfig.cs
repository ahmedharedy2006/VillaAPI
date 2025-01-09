using AutoMapper;
using VillaAPI.Models;
using VillaAPI.Models.Auth;
using VillaAPI.Models.DTO;
using VillaAPI.Models.DTO.Auth;

namespace VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Villa , VillaDTO>().ReverseMap();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();

            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<User, LoginRequestDTO>().ReverseMap();

            CreateMap<User, RegisterRequestDTO>().ReverseMap();


        }
    }
}

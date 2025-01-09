using AutoMapper;
using webApp.Models;
using webApp.Models.Auth;
using webApp.Models.DTO;
using webApp.Models.DTO.Auth;
    
namespace webApp
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<VillaCreateDTO , VillaDTO>().ReverseMap();

            CreateMap<VillaUpdateDTO, VillaDTO>().ReverseMap();




        }
    }
}

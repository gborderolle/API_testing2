using API_testing2.Models;
using API_testing2.Models.Dto;
using AutoMapper;

namespace API_testing2.Services
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<VillaCreateDto, Villa>().ReverseMap();
        }
    }
}

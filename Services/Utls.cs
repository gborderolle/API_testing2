using API_testing2.Models;
using API_testing2.Models.Dto;
using AutoMapper;

namespace API_testing1.Services
{
    public class Utls
    {

        private static MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<VillaDto, Villa>();
            cfg.CreateMap<Villa, VillaDto>();

            cfg.CreateMap<VillaCreateDto, Villa>();
            cfg.CreateMap<Villa, VillaCreateDto>();

            cfg.CreateMap<VillaUpdateDto, Villa>();
            cfg.CreateMap<Villa, VillaUpdateDto>();
        });

        public static Mapper mapper { get; set; } = new(config);

    }

}
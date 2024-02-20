using AutoMapper;
using DtoLayer.SubAboutDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.SubAbouts
{
    public class SubAboutMapper : Profile
    {
        public SubAboutMapper()
        {
            CreateMap<SubAbout, ResultSubAboutDto>().ReverseMap();
        }
    }
}

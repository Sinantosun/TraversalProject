using AutoMapper;
using DtoLayer.GuideDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Guides
{
    public class GuideMapper:Profile
    {
        public GuideMapper()
        {
            CreateMap<Guide, ResultGuideDto>().ReverseMap();
        }
    }
}

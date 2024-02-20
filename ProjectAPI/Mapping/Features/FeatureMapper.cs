using AutoMapper;
using DtoLayer.FeaturesDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Features
{
    public class FeatureMapper : Profile
    {
        public FeatureMapper()
        {
            CreateMap<Feature, ResultFeatrueDto>().ReverseMap();
        }
    }
}

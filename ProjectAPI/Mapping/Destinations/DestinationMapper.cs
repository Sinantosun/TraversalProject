using AutoMapper;
using DtoLayer.DestinationDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Destinations
{
    public class DestinationMapper : Profile
    {
        public DestinationMapper()
        {
            CreateMap<Destination, ResultDestinationDto>().ReverseMap();
            CreateMap<Destination, ResultByIdForDestinationDto>().ReverseMap();
            CreateMap<Destination, CreateDestinationDto>().ReverseMap();
            CreateMap<Destination, UpdateDestinationDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using DtoLayer.AnnouncementDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Announcements
{
    public class AnnouncementMapper:Profile
    {
        public AnnouncementMapper()
        {
            CreateMap<Announcement, CreateAnnouncementDto>().ReverseMap();
            CreateMap<Announcement, ResultAnnouncumentDto>().ReverseMap();
            CreateMap<Announcement, UpdateAnnouncumentDto>().ReverseMap();
        }
    }
}

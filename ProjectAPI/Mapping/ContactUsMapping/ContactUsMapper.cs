using AutoMapper;
using DtoLayer.ContactUSDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.ContactUsMapping
{
    public class ContactUsMapper : Profile
    {
        public ContactUsMapper()
        {
            CreateMap<ContactUs, CreateContactUSDto>().ReverseMap();
        }
    }
}

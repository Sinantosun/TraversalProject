using AutoMapper;
using DtoLayer.TestimonailDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Testimonails
{
    public class TestimonialMapper : Profile
    {
        public TestimonialMapper()
        {
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
        }
    }
}

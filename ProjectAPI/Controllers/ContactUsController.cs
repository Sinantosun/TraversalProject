using AutoMapper;
using BussinessLayer.AbstractValidator;
using DtoLayer.ContactUSDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsSerivce _contactUsSerivce;
        private readonly IMapper _mapper;
        public ContactUsController(IContactUsSerivce contactUsSerivce, IMapper mapper)
        {
            _contactUsSerivce = contactUsSerivce;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult getContactUsList()
        {
            var values = _contactUsSerivce.TgetListContactUsByTrue().OrderByDescending(x=>x.MessageDate);
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateContact(CreateContactUSDto createContactUSDto)
        {
            var mappedValues = _mapper.Map<ContactUs>(createContactUSDto);
            _contactUsSerivce.TInsert(mappedValues);
            return Ok("eklendi");
        }
    }
}

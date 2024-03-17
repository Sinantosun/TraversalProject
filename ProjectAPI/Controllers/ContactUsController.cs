using AutoMapper;
using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.ContactUSValidators;
using DtoLayer.ContactUSDtos;
using DtoLayer.GenericNotificationDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
            var values = _contactUsSerivce.TgetListContactUsByTrue().OrderByDescending(x => x.MessageDate);
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateContact(CreateContactUSDto createContactUSDto)
        {
            SendContactUsValidator validationRules = new SendContactUsValidator();
            ValidationResult result = validationRules.Validate(createContactUSDto);
            if (result.IsValid)
            {
                var mappedValues = _mapper.Map<ContactUs>(createContactUSDto);
                _contactUsSerivce.TInsert(mappedValues);
                return Ok("eklendi");
            }
            else
            {
                List<ResultNotificationDto> notfiy = new List<ResultNotificationDto>();
                foreach (var item in result.Errors)
                {
                    notfiy.Add(new ResultNotificationDto { Description = item.ErrorMessage, PropertyName = item.PropertyName });
                }
                return BadRequest(notfiy);
            }


        }
    }
}

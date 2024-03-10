using BussinessLayer.AbstractValidator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsSerivce _contactUsSerivce;

        public ContactUsController(IContactUsSerivce contactUsSerivce)
        {
            _contactUsSerivce = contactUsSerivce;
        }

        [HttpGet]
        public IActionResult getContactUsList()
        {
            var values = _contactUsSerivce.TgetListContactUsByTrue().OrderByDescending(x=>x.MessageDate);
            return Ok(values);
        }
    }
}

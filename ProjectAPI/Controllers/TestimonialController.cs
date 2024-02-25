using AutoMapper;
using BussinessLayer.AbstractValidator;
using DtoLayer.TestimonailDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly ITestimionalService _testimionalService;

        public TestimonialController(IMapper ıMapper, ITestimionalService testimionalService)
        {
            _IMapper = ıMapper;
            _testimionalService = testimionalService;
        }

        [HttpGet]
        public IActionResult GetTestimionalList()
        {
            var mappedValues = _IMapper.Map<List<ResultTestimonialDto>>(_testimionalService.TGetList());
            return Ok(mappedValues);
        }
    }
}

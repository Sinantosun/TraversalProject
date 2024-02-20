using AutoMapper;
using BussinessLayer.Abstract;
using DtoLayer.SubAboutDtos;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAboutController : ControllerBase
    {
        private readonly ISubAboutService _ISubAboutService;
        private readonly IMapper _mapper;

        public SubAboutController(ISubAboutService ıSubAboutService, IMapper mapper)
        {
            _ISubAboutService = ıSubAboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSubAboutsList()
        {
            var mappedValues = _mapper.Map<List<ResultSubAboutDto>>(_ISubAboutService.TGetList());
            return Ok(mappedValues);
        }
    }
}

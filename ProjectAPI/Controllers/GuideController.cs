using AutoMapper;
using BussinessLayer.AbstractValidator;
using DtoLayer.GuideDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly IGuideService _guideService;
        private readonly IMapper _mapper;
        public GuideController(IGuideService guideService, IMapper mapper)
        {
            _guideService = guideService;
            _mapper = mapper;
        }

        [HttpGet("GetGuideList")]
        public IActionResult GetGuideList()
        {
            var mappedValues = _mapper.Map<List<ResultGuideDto>>(_guideService.TGetList());
            return Ok(mappedValues);
        }
    }
}

using AutoMapper;
using BussinessLayer.Abstract;
using DtoLayer.FeaturesDtos;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;
        public FeaturesController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetFeaturesList()
        {
            var values = _mapper.Map<List<ResultFeatrueDto>>(_featureService.TGetList());
            return Ok(values);
        }
    }
}

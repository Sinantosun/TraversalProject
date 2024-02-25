using AutoMapper;
using BussinessLayer.AbstractValidator;
using DtoLayer.DestinationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;
        private readonly IMapper mapper;

        public DestinationsController(IDestinationService destinationService, IMapper mapper)
        {
            _destinationService = destinationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDestinationList()
        {
            var value = mapper.Map<List<ResultDestinationDto>>(_destinationService.TGetList());
            return Ok(value);
        }

        [HttpGet("{id}")]
        public IActionResult getDestinationByID(int id)
        {
            var value = mapper.Map<ResultByIdForDestinationDto>(_destinationService.TGetById(id));
            return Ok(value);
        }
    }
}

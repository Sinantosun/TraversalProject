using AutoMapper;
using BussinessLayer.AbstractValidator;
using DtoLayer.DestinationDtos;
using EntityLayer.Concrete;
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
        [HttpPost]
        public IActionResult CreateDestination(CreateDestinationDto createDestinationDto)
        {
            try
            {
                createDestinationDto.Status = true;
                var mapValue = mapper.Map<Destination>(createDestinationDto);
                _destinationService.TInsert(mapValue);
                return Ok();
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateDestination(UpdateDestinationDto createDestinationDto)
        {
            try
            {
                createDestinationDto.Status = true;
                var mapValue = mapper.Map<Destination>(createDestinationDto);
                _destinationService.TUpdate(mapValue);
                return Ok();
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
        [HttpDelete]
        public IActionResult DeleteDestination(int id)
        {
            try
            {
                 var find = _destinationService.TGetById(id);
                _destinationService.TDelete(find);
                return Ok();
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

    }
}

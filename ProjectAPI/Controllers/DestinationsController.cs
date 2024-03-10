using AutoMapper;
using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.DestinationValidatior;
using DtoLayer.DestinationDtos;
using DtoLayer.GenericNotificationDtos;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
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
        [HttpGet("getDestinationByCityName/{name}")]
        public IActionResult getDestinationByCityName(string name)
        {
            var value = mapper.Map<ResultByCityNameForDestinationDto>(_destinationService.TgetDestinationByCityName(name));
            if (value != null)
            {
                return Ok(value);
            }
            else
            {
                return BadRequest("Bulunamadı");
            }
        }

        [HttpPut("updateDestinationByCityName")]
        public IActionResult updateDestinationByCityName(UpdateDestinationDto updateDestinationDto,string name)
        {
            var value = _destinationService.TgetDestinationByCityName(name);
            if (value != null)
            {
                value.Capacity = updateDestinationDto.Capacity;
                value.City=updateDestinationDto.City;
                value.CoverImage=updateDestinationDto.CoverImage;
                value.DayNight=updateDestinationDto.DayNight;
                value.Description=updateDestinationDto.Description;
                value.DestinationID=updateDestinationDto.DestinationID;
                value.Details1=updateDestinationDto.Details1;
                value.Details2=updateDestinationDto.Details2;
                value.Image=updateDestinationDto.Image;
                value.Price=updateDestinationDto.Price;
                
                _destinationService.TUpdate(value);
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete("deleteDestinationByCityName/{name}")]
        public IActionResult deleteDestinationByCityName(string name)
        {
            var value = _destinationService.TgetDestinationByCityName(name);
            if (value != null)
            {
                _destinationService.TdeleteDestinationByCityName(name);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult CreateDestination(CreateDestinationDto createDestinationDto)
        {

            DestinationValidators validationRules = new DestinationValidators();
            ValidationResult validationResult = validationRules.Validate(createDestinationDto);
            if (validationResult.IsValid)
            {
                createDestinationDto.Status = true;
                var mapValue = mapper.Map<Destination>(createDestinationDto);
                _destinationService.TInsert(mapValue);
                return Ok();
            }
            else
            {
                List<ResultNotificationDto> ErrorList = new List<ResultNotificationDto>();
                foreach (var item in validationResult.Errors)
                {
                    ErrorList.Add(new ResultNotificationDto()
                    {
                        Description = item.ErrorMessage,
                        PropertyName = item.PropertyName
                    });
                }
                return BadRequest(ErrorList);
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

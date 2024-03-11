using AutoMapper;
using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.AnnouncementIValidators;
using DtoLayer.AnnouncementDtos;
using DtoLayer.GenericNotificationDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnounecementService _announecementService;
        private readonly IMapper _mapper;

        public AnnouncementController(IAnnounecementService announecementService, IMapper mapper)
        {
            _announecementService = announecementService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult AnnouncementById(int id)
        {
            var values = _announecementService.TGetById(id);
            return Ok(values);
        }


        [HttpGet]
        public IActionResult AnnouncementList()
        {
            var values = _mapper.Map<List<ResultAnnouncumentDto>>(_announecementService.TGetList());
            return Ok(values);
        }
        [HttpDelete]
        public IActionResult AnnouncementDelete(int id)
        {
            var values = _announecementService.TGetById(id);
            _announecementService.TDelete(values);
            return Ok();
        }

        [HttpPut]
        public IActionResult AnnouncementUpdate(UpdateAnnouncumentDto updateAnnouncumentDto)
        {
            var mappedValues = _mapper.Map<Announcement>(updateAnnouncumentDto);
            _announecementService.TUpdate(mappedValues);
            return Ok();
        }


        [HttpPost]
        public IActionResult AnnouncementAdd(CreateAnnouncementDto createAnnouncementDto)
        {

            CreateAnnouncementIValidator validationRules = new CreateAnnouncementIValidator();
            ValidationResult validationResult = validationRules.Validate(createAnnouncementDto);
            if (validationResult.IsValid)
            {
                var mappedValues = _mapper.Map<Announcement>(createAnnouncementDto);
                _announecementService.TInsert(mappedValues);
                return Ok();
            }
            else
            {
                List<ResultNotificationDto> ResultNotificationDto = new List<ResultNotificationDto>();
                ResultNotificationDto.Clear();
                foreach (var item in validationResult.Errors)
                {
                    ResultNotificationDto.Add(new ResultNotificationDto()
                    {
                        Description=item.ErrorMessage,
                        PropertyName=item.PropertyName
                    });
                }

                return BadRequest(ResultNotificationDto);
            }

        }
    }
}

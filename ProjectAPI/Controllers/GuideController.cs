﻿using AutoMapper;
using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.GuideValidator;
using DtoLayer.GenericNotificationDtos;
using DtoLayer.GuideDtos;
using DtoLayer.TestimonailDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
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

        [HttpPost]
        public IActionResult CreateGuide(CreateGuideDto createGuideDto)
        {
            CreateGuideValidator validationRules = new CreateGuideValidator();
            ValidationResult validationResult = validationRules.Validate(createGuideDto);
            if (validationResult.IsValid)
            {
                var mappedValues = _mapper.Map<Guide>(createGuideDto);
                _guideService.TInsert(mappedValues);
                return Ok();
            }
            else
            {
                List<ResultNotificationDto> ErrorList = new List<ResultNotificationDto>();
                foreach (var item in validationResult.Errors)
                {
                    ResultNotificationDto ErorrListDetails = new ResultNotificationDto();
                    ErorrListDetails.PropertyName = item.PropertyName;
                    ErorrListDetails.Description = item.ErrorMessage;
                    ErrorList.Add(ErorrListDetails);
                }


                return BadRequest(ErrorList);
            }


        }

        [HttpPut]
        public IActionResult EditGuide(UpdateGuideDto updateGuideDto)
        {
            var mappedValues = _mapper.Map<Guide>(updateGuideDto);
            _guideService.TUpdate(mappedValues);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetGuideByID(int id)
        {
            var mappedValues = _guideService.TGetById(id);
            return Ok(mappedValues);
        }
    }
}

using AutoMapper;
using BussinessLayer.AbstractValidator;
using BussinessLayer.ValidationRules.ReservationValidatior;
using DtoLayer.GenericNotificationDtos;
using DtoLayer.ReservationDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResarvationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ResarvationController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet("SelectListForDropdown")]
        public IActionResult SelectListForDropdown()
        {
            // List<SelectListItem> value = (from x in _destinationService.TGetList() select new SelectListItem {Text=x.City,Value=x.DestinationID.ToString() }).ToList();
            return Ok();
        }

        [HttpGet("GetApproveReservations/id")]
        public IActionResult GetApproveReservations(int id)
        {
            var mappedValues = _mapper.Map<List<ResultReservationByIdDto>>(_reservationService.TGetListWithReservationByWaitApproval(id));
            return Ok(mappedValues);
        }

        [HttpGet("GetCurrentReservations/id")]
        public IActionResult GetCurrentReservations(int id)
        {
            var mappedValues = _mapper.Map<List<ResultReservationByIdDto>>(_reservationService.TGetListWithReservationByAccepted(id));
            return Ok(mappedValues);
        }

        [HttpGet("GetOldReservation/id")]
        public IActionResult GetOldReservation(int id)
        {
            var mappedValues = _mapper.Map<List<ResultReservationByIdDto>>(_reservationService.TGetListWithReservationByPrevious(id));
            return Ok(mappedValues);
        }

        [HttpPost]
        public IActionResult CreateReservation(CreateResarvationDto createResarvationDto)
        {
            if (createResarvationDto.Description == null)
            {
                createResarvationDto.Description = "Açıklama Yok";
            }

            ReservationValidatiors validationRules = new ReservationValidatiors();
            ValidationResult validationResult = validationRules.Validate(createResarvationDto);
            if (validationResult.IsValid)
            {
                Reservation comment = new Reservation()
                {
                    AppUserId = createResarvationDto.AppUserId,
                    Description = createResarvationDto.Description,
                    DestinationID = createResarvationDto.DestinationID,
                    PersonCount = createResarvationDto.PersonCount,
                    ReservastionDate = createResarvationDto.ReservastionDate,
                    Status = createResarvationDto.Status,
                };
                _reservationService.TInsert(comment);
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
    }
}

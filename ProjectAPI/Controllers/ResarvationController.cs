using BussinessLayer.AbstractValidator;
using DtoLayer.ReservationDtos;
using EntityLayer.Concrete;
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

        public ResarvationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("SelectListForDropdown")]
        public IActionResult SelectListForDropdown()
        {
           // List<SelectListItem> value = (from x in _destinationService.TGetList() select new SelectListItem {Text=x.City,Value=x.DestinationID.ToString() }).ToList();
            return Ok();
        }


        [HttpPost]
        public IActionResult CreateReservation(CreateResarvationDto createResarvationDto)
        {

            Reservation comment = new Reservation()
            {
               AppUserId=createResarvationDto.AppUserId,
               Description=createResarvationDto.Description,
               Destination=createResarvationDto.Destination,
               PersonCount=createResarvationDto.PersonCount,
               ReservastionDate=createResarvationDto.ReservastionDate,
               Status=createResarvationDto.Status,
               
               

            };
            _reservationService.TInsert(comment);
            return Ok();
        }
    }
}

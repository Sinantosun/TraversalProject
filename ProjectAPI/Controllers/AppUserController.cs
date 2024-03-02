using AutoMapper;
using BussinessLayer.Abstract;
using BussinessLayer.AbstractValidator;
using DtoLayer.AppUserDtos;
using DtoLayer.DestinationDtos;
using DtoLayer.ReservationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppuserService _appuserService;
        private readonly IReservationService reservationService;
        private readonly IMapper _mapper;
        public AppUserController(IAppuserService appuserService, IReservationService reservationService, IMapper mapper)
        {
            _appuserService = appuserService;
            this.reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var values = _appuserService.getListAppUserWithx();

            return Ok(values);
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var values = _appuserService.TGetById(id);
            _appuserService.TDelete(values);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult FindByIdUser(int id)
        {
            var values = _appuserService.TGetById(id);
            ResultUserDto resultUserDto = new ResultUserDto()
            {
                Email = values.Email,
                Id = values.Id,
                Name = values.Name,
                Surname = values.Surname,
            };
            return Ok(resultUserDto);
        }

        [HttpGet("UserReservationLists/{id}")]
        public IActionResult UserReservationLists(int id)
        {
            var values = _mapper.Map<List<ResultReservationByIdDto>>(reservationService.TGetListWithReservationByAccepted(id));
            return Ok(values);
        }
    }
}

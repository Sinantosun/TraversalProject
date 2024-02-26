using AutoMapper;
using DtoLayer.ReservationDtos;
using EntityLayer.Concrete;

namespace ProjectAPI.Mapping.Reservations
{
    public class ReservationsMapper : Profile
    {
        public ReservationsMapper()
        {
            CreateMap<Reservation, ResultReservationByIdDto>().ReverseMap();
        }
    }
}

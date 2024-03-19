using EntityLayer.Concrete;

namespace TraversalProject.Dtos.ReservationDtos
{
    public class ResultReservationDto
    {
        public int? ReservationID { get; set; }
        public string NameSurname { get; set; }
        public string City { get; set; }
        public string DayNight { get; set; }
        public string PersonCount { get; set; }
        public DateTime? ReservastionDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}

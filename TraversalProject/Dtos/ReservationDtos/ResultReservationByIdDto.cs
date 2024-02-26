using EntityLayer.Concrete;

namespace TraversalProject.Dtos.ReservationDtos
{
    public class ResultReservationByIdDto
    {
        public int ReservationID { get; set; }
        public int AppUserId { get; set; }
        public Destination Destination { get; set; }
        public int DestinationID { get; set; }
        public string PersonCount { get; set; }
        public string Destinations { get; set; }
        public DateTime ReservastionDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}

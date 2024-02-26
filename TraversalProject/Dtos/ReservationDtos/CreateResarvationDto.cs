using EntityLayer.Concrete;

namespace TraversalProject.Dtos.ReservationDtos
{
    public class CreateResarvationDto
    {
        public int AppUserId { get; set; }
        public string PersonCount { get; set; }
        public string Destination { get; set; }
        public DateTime ReservastionDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}

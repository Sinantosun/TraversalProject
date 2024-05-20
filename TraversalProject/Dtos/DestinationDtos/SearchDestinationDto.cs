

using EntityLayer.Concrete;
using System.ComponentModel.DataAnnotations;

namespace TraversalProject.Dtos.DestinationDtos
{
    public class SearchDestinationDto
    {
        public string DestinationID { get; set; }
        public string City { get; set; }
        public string DestinationDate { get; set; }

    }
}

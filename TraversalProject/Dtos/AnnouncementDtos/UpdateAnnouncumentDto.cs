
namespace TraversalProject.Dtos.AnnouncementDtos
{
    public class UpdateAnnouncumentDto
    {
        public int AnnouncementID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }
    }
}

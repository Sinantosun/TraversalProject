
namespace DtoLayer.CommentDtos
{
    public class CreateCommandDto
    {
        public string CommentUser { get; set; }
        public string CommentContent { get; set; }
        public int DestinationID { get; set; }
    }
}

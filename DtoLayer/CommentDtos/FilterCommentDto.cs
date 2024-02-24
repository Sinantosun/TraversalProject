
namespace DtoLayer.CommentDtos
{
    public class FilterCommentDto
    {
        public int CommentID { get; set; }
        public string CommentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }

        public int DestinationID { get; set; }
    }
}



using EntityLayer.Concrete;

namespace TraversalProject.Dtos.CommentDtos
{
    public class ResultCommentDto
    {
        public int CommentID { get; set; }
        public string CommentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }
        public Destination Destination { get; set; }
        public int DestinationID { get; set; }
    }
}

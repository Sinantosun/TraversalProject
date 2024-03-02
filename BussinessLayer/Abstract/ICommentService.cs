using EntityLayer.Concrete;

namespace BussinessLayer.AbstractValidator
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> TGetDestinationByID(int id);
        List<Comment> TgetAllCommentWithDestination();
    }
}

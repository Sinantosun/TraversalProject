using EntityLayer.Concrete;

namespace BussinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        List<Comment> TGetDestinationByID(int id);
    }
}

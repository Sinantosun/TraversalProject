using EntityLayer.Concrete;

namespace DataAccsesLayer.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        List<Comment> getAllCommentWithDestination();
    }
}

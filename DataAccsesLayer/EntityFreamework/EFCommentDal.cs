using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EFCommentDal(Context context) : base(context)
        {
        }

        public List<Comment> getAllCommentWithDestination()
        {
            using var context = new Context();
            var result = context.Comments.Include(y => y.Destination).ToList();
            return result;
        }
    }
}

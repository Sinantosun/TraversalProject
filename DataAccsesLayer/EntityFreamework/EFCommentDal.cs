using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EFCommentDal(Context context) : base(context)
        {
        }
    }
}

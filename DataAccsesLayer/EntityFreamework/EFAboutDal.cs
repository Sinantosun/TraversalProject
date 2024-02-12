using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFAboutDal : GenericRepository<About>, IAboutDal
    {
        public EFAboutDal(Context context) : base(context)
        {
        }
    }
}

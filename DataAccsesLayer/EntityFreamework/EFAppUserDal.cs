
using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFAppUserDal : GenericRepository<AppUser>, IAppUserDal
    {
        public EFAppUserDal(Context context) : base(context)
        {
        }
    }
}

using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFNewsletterDal : GenericRepository<Newsletter>, INewsletterDal
    {
        public EFNewsletterDal(Context context) : base(context)
        {
        }
    }
}

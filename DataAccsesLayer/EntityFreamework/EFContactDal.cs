using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFContactDal : GenericRepository<Contact>, IContactDal
    {
        public EFContactDal(Context context) : base(context)
        {
        }
    }
}

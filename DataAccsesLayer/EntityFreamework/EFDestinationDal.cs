using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFDestinationDal : GenericRepository<Destination>, IDestinationDal
    {
        public EFDestinationDal(Context context) : base(context)
        {
        }
    }
}

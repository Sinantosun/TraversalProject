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

        public int getDestinationCount()
        {
            using var context = new Context();
            return context.Destinations.Count();
        }
    }
}

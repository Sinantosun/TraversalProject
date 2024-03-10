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

        public void deleteDestinationByCityName(string name)
        {
            using var context = new Context();
            var value = context.Destinations.FirstOrDefault(x => x.City == name);
            context.Destinations.Remove(value);
            context.SaveChanges();
        }

        public Destination getDestinationByCityName(string name)
        {
            using var context = new Context();
            return context.Destinations.FirstOrDefault(x => x.City == name);
        }

        public int getDestinationCount()
        {
            using var context = new Context();
            return context.Destinations.Count();
        }
    }
}

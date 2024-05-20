using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;
using System.Xml.Linq;

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

        public List<Destination> getDestinationBySearchFilter(int CityID, string dateTime)
        {
            DateTime dt = Convert.ToDateTime(dateTime);
            using var context = new Context();
            return context.Destinations.Where(x => x.DestinationID == CityID && x.DestinationDate == dt).ToList();
        }

        public int getDestinationCount()
        {
            using var context = new Context();
            return context.Destinations.Count();
        }

        public List<Destination> getFourLastDestination()
        {
            using var context = new Context();
            return context.Destinations.OrderByDescending(x => x.DestinationID).Take(4).ToList();
        }
    }
}

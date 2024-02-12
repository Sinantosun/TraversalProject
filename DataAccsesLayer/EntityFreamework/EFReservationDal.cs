using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;


namespace DataAccsesLayer.EntityFreamework
{
    public class EFReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        public EFReservationDal(Context context) : base(context)
        {
        }
    }
}

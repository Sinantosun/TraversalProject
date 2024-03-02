using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        public EFReservationDal(Context context) : base(context)
        {
        }

        public List<Reservation> GetListWithReservationByAccepted(int id)
        {
            using var context = new Context();
            return context.Reservations.Include(x => x.Destination).Include(y => y.AppUser).Where(x => x.Status == "Onaylandı" && x.AppUserId == id).ToList();
        }

        public List<Reservation> GetListWithReservationByPrevious(int id)
        {
            using var context = new Context();
            return context.Reservations.Include(x => x.Destination).Where(x => x.Status == "Geçmiş Rezervasyon" && x.AppUserId == id).ToList();
        }

        public List<Reservation> GetListWithReservationByWaitApproval(int id)
        {

            using var context = new Context();
            return context.Reservations.Include(x => x.Destination).Where(x => x.Status == "Onay Bekliyor" && x.AppUserId == id).ToList();
        }
    }
}

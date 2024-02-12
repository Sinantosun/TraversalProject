using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFAnnouncementDal : GenericRepository<Announcement>, IAnnouncementDal
    {
        public EFAnnouncementDal(Context context) : base(context)
        {
        }
    }
}

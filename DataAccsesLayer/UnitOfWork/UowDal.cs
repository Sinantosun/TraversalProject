using DataAccsesLayer.Concrete;

namespace DataAccsesLayer.UnitOfWork
{
    public class UowDal : IUowDal
    {
        private readonly Context _context;

        public UowDal(Context context)
        {
            _context = context;
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}

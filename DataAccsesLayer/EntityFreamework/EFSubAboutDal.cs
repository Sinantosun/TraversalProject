using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;


namespace DataAccsesLayer.EntityFreamework
{
    public class EFSubAboutDal : GenericRepository<SubAbout>, ISubAboutDal
    {
        public EFSubAboutDal(Context context) : base(context)
        {
        }
    }
}

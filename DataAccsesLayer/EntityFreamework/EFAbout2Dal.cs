using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;
namespace DataAccsesLayer.EntityFreamework
{
    public class EFAbout2Dal : GenericRepository<About2>, IAbout2Dal
    {
        public EFAbout2Dal(Context context) : base(context)
        {
        }
    }
}

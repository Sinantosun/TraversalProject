using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFFeature2Dal : GenericRepository<Feature2>, IFeature2Dal
    {
        public EFFeature2Dal(Context context) : base(context)
        {
        }
    }
}

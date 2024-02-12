using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;


namespace DataAccsesLayer.EntityFreamework
{
    public class EFFeatureDal : GenericRepository<Feature>, IFeatureDal
    {
        public EFFeatureDal(Context context) : base(context)
        {
        }
    }
}

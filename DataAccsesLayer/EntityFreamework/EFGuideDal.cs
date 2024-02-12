using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;


namespace DataAccsesLayer.EntityFreamework
{
    public class EFGuideDal : GenericRepository<Guide>, IGuideDal
    {
        public EFGuideDal(Context context) : base(context)
        {
        }
    }
}

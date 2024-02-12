using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;


namespace DataAccsesLayer.EntityFreamework
{
    public class EFAccountDal : GenericRepository<Account>, IAccountDal
    {
        public EFAccountDal(Context context) : base(context)
        {
        }
    }
}

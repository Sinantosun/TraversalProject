using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFTestimonialDal : GenericRepository<Testimonial>, ITestimonialDal
    {
        public EFTestimonialDal(Context context) : base(context)
        {
        }
    }
}

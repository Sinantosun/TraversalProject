using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DataAccsesLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsesLayer.EntityFreamework
{
    public class EFContactUsDal : GenericRepository<ContactUs>, IContactUsDal
    {
        public EFContactUsDal(Context context) : base(context)
        {
        }
    }
}

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

        public void ContactUsStatusChangeToFalse(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContactUs> getListContactUsByFalse()
        {
            using var context = new Context();
            var values = context.ContactUses.Where(x => x.MessageStatus == false).ToList();
            return values;
        }

        public List<ContactUs> getListContactUsByTrue()
        {
            using var context = new Context();
            var values = context.ContactUses.Where(x => x.MessageStatus == true).ToList();
            return values;
        }
    }
}

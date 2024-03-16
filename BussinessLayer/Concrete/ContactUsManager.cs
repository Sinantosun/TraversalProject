
using BussinessLayer.AbstractValidator;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;

namespace BussinessLayer.Concrete
{
    public class ContactUsManager : IContactUsSerivce
    {

        private readonly IContactUsDal _contactUsDal;

        public ContactUsManager(IContactUsDal contactUsDal)
        {
            _contactUsDal = contactUsDal;
        }

        public void TContactUsStatusChangeToFalse(int id)
        {
           _contactUsDal.ContactUsStatusChangeToFalse(id);
        }

        public void TDelete(ContactUs t)
        {
            _contactUsDal.Delete(t);
        }

        public ContactUs TGetById(int id)
        {
            return _contactUsDal.GetById(id);
        }

        public List<ContactUs> TGetList()
        {
            return _contactUsDal.GetList();
        }

        public List<ContactUs> TgetListContactUsByFalse()
        {
            return _contactUsDal.getListContactUsByFalse();
        }

        public List<ContactUs> TgetListContactUsByTrue()
        {
            return _contactUsDal.getListContactUsByTrue();
        }

        public void TInsert(ContactUs t)
        {
            _contactUsDal.Insert(t);
            
        }

        public void TUpdate(ContactUs t)
        {
            _contactUsDal.Update(t);
        }
    }
}

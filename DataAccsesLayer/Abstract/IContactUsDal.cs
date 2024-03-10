using EntityLayer.Concrete;

namespace DataAccsesLayer.Abstract
{
    public interface IContactUsDal : IGenericDal<ContactUs>
    {
        List<ContactUs> getListContactUsByTrue();
        List<ContactUs> getListContactUsByFalse();
        void ContactUsStatusChangeToFalse(int id);
    }
}

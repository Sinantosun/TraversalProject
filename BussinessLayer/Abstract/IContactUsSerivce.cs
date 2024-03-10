
using EntityLayer.Concrete;
namespace BussinessLayer.AbstractValidator
{
    public interface IContactUsSerivce : IGenericService<ContactUs>
    {
        List<ContactUs> TgetListContactUsByTrue();
        List<ContactUs> TgetListContactUsByFalse();
        void TContactUsStatusChangeToFalse(int id);
    }
}

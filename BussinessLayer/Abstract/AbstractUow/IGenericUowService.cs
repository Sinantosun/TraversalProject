

using EntityLayer.Concrete;

namespace BussinessLayer.Abstract.AbstractUow
{
    public interface IGenericUowService<T> where T : class
    {
        void TInsert(T t);
        void TUpdate(T t);
        void TMultiUpdate(List<T> t);
        T TgetByID(int id);
        List<Account> TListAccounts();


    }
}

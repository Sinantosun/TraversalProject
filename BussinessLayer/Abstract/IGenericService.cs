using System.Linq.Expressions;

namespace BussinessLayer.AbstractValidator
{
    public interface IGenericService<T> where T : class
    {
        void TInsert(T t);
        void TDelete(T t);
        void TUpdate(T t);
        T TGetById(int id);
        List<T> TGetList();

    }
}

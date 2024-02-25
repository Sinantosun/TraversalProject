using BussinessLayer.AbstractValidator;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;
using System.Linq.Expressions;

namespace BussinessLayer.Concrete
{
    public class DestinationManager : IDestinationService
    {
        private readonly IDestinationDal _IDestinationDal;

        public DestinationManager(IDestinationDal ıDestinationDal)
        {
            _IDestinationDal = ıDestinationDal;
        }

        public void TDelete(Destination t)
        {
            _IDestinationDal.Delete(t);
        }

        public Destination TGetById(int id)
        {
            return _IDestinationDal.GetById(id);
        }

        public int TGetDestinationService()
        {
            return _IDestinationDal.getDestinationCount();
        }

        public List<Destination> TGetList()
        {
            return _IDestinationDal.GetList();
        }

        public List<Destination> TGetListByFilter(Expression<Func<Destination, bool>> filter)
        {
            return _IDestinationDal.GetListByFilter(filter);
        }

        public void TInsert(Destination t)
        {
            _IDestinationDal.Insert(t);
        }

        public void TUpdate(Destination t)
        {
            _IDestinationDal.Update(t);
        }
    }
}

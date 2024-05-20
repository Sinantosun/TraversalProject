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

        public void TdeleteDestinationByCityName(string name)
        {

            _IDestinationDal.deleteDestinationByCityName(name);
        }

        public Destination TGetById(int id)
        {
            return _IDestinationDal.GetById(id);
        }

        public Destination TgetDestinationByCityName(string name)
        {
            return _IDestinationDal.getDestinationByCityName(name);
        }

        public List<Destination> TgetDestinationBySearchFilter(int CityID, string dateTime)
        {
            return _IDestinationDal.getDestinationBySearchFilter(CityID, dateTime);
        }

        public int TGetDestinationService()
        {
            return _IDestinationDal.getDestinationCount();
        }

        public List<Destination> TgetFourLastDestination()
        {
           return _IDestinationDal.getFourLastDestination();
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

using BussinessLayer.Abstract;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;
using System.Linq.Expressions;

namespace BussinessLayer.Concrete
{
    public class SubAboutManager : ISubAboutService
    {
        private readonly ISubAboutDal _subAboutDal;

        public SubAboutManager(ISubAboutDal subAboutDal)
        {
            _subAboutDal = subAboutDal;
        }

        public void TDelete(SubAbout t)
        {
            _subAboutDal.Delete(t);
        }

        public SubAbout TGetById(int id)
        {
          return  _subAboutDal.GetById(id);
        }

        public List<SubAbout> TGetList()
        {
            return _subAboutDal.GetList();
        }

        public List<SubAbout> TGetListByFilter(Expression<Func<SubAbout, bool>> filter)
        {
            return _subAboutDal.GetListByFilter(filter);
        }

        public void TInsert(SubAbout t)
        {
            _subAboutDal.Insert(t);
        }

        public void TUpdate(SubAbout t)
        {
            _subAboutDal.Update(t);
        }
    }
}

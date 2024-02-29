using BussinessLayer.AbstractValidator;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;

namespace BussinessLayer.Concrete
{
    public class GuideManager : IGuideService
    {
        private readonly IGuideDal _guideDal;

        public GuideManager(IGuideDal guideDal)
        {
            _guideDal = guideDal;
        }

        public void TDelete(Guide t)
        {
            _guideDal.Delete(t);
        }

        public Guide TGetById(int id)
        {
           return _guideDal.GetById(id);
        }

        public List<Guide> TGetList()
        {
            return _guideDal.GetList();
        }

        public void TInsert(Guide t)
        {
            _guideDal.Insert(t);
        }

        public void TUpdate(Guide t)
        {
            _guideDal.Update(t);
        }
    }
}

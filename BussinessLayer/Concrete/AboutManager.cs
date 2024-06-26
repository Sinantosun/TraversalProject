﻿
using BussinessLayer.AbstractValidator;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;
using System.Linq.Expressions;

namespace BussinessLayer.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _IAboutDal;

        public AboutManager(IAboutDal ıAboutDal)
        {
            _IAboutDal = ıAboutDal;
        }

        public void TDelete(About t)
        {
            _IAboutDal.Delete(t);
        }

        public About TGetById(int id)
        {
            return _IAboutDal.GetById(id);
        }

        public List<About> TGetList()
        {
            return _IAboutDal.GetList();
        }

        public List<About> TGetListByFilter(Expression<Func<About, bool>> filter)
        {
            return _IAboutDal.GetListByFilter(filter);
        }

        public void TInsert(About t)
        {
            _IAboutDal.Insert(t);
        }

        public void TUpdate(About t)
        {
            _IAboutDal.Update(t);
        }
    }
}

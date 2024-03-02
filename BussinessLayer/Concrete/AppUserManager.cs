using BussinessLayer.Abstract;
using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using DtoLayer.AppUserDtos;
using EntityLayer.Concrete;

namespace BussinessLayer.Concrete
{

    public class AppUserManager : IAppuserService
    {
        private readonly IAppUserDal _appUserDal;

        public AppUserManager(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }


        public void TDelete(AppUser t)
        {
            _appUserDal.Delete(t);
        }

        public AppUser TGetById(int id)
        {
            return _appUserDal.GetById(id);
        }

        public List<AppUser> TGetList()
        {
            return _appUserDal.GetList();
        }

        public void TInsert(AppUser t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(AppUser t)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ResultUserDto> IAppuserService.getListAppUserWithx()
        {
            using var context = new Context();
            return context.Users.Select(item => new ResultUserDto
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                Email = item.Email


            }).ToList();
        }
    }
}

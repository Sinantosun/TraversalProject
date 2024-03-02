
using BussinessLayer.AbstractValidator;
using DtoLayer.AppUserDtos;
using EntityLayer.Concrete;

namespace BussinessLayer.Abstract
{
    public interface IAppuserService : IGenericService<AppUser>
    {


        IEnumerable<ResultUserDto> getListAppUserWithx();
    }
}

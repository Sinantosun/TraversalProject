

using BussinessLayer.Abstract.AbstractUow;
using DataAccsesLayer.Abstract;
using DataAccsesLayer.UnitOfWork;
using EntityLayer.Concrete;

namespace BussinessLayer.Concrete.UowConcrete
{
    public class AccountOuwManager : IAccountServiceUow
    {
        private readonly IAccountDal _accountDal;
        private readonly IUowDal _uowDal;

        public AccountOuwManager(IAccountDal accountDal, IUowDal uowDal)
        {
            _accountDal = accountDal;
            _uowDal = uowDal;
        }

        public Account TgetByID(int id)
        {
            return _accountDal.getByID(id);
        }

        public void TInsert(Account t)
        {
            _accountDal.Insert(t);
            _uowDal.save();
        }

        public List<Account> TListAccounts()
        {
            return _accountDal.ListAccounts();
        }

        public void TMultiUpdate(List<Account> t)
        {
            _accountDal.MultiUpdate(t);
            _uowDal.save();
        }

        public void TUpdate(Account t)
        {

            _accountDal.Update(t);
            _uowDal.save();
        }
    }
}

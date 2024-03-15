
using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccsesLayer.Repository
{
    public class GenericUowRepository<T> : IGenericUowDal<T> where T : class
    {
        private readonly Context _context;

        public GenericUowRepository(Context context)
        {
            _context = context;
        }

        public void Update(T t)
        {
            _context.Update(t);
        }

        public void Insert(T t)
        {
            _context.Add(t);
        }

        public void MultiUpdate(List<T> t)
        {
            _context.UpdateRange(t);
        }

        public T getByID(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<Account> ListAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}

using DataAccsesLayer.Abstract;
using DataAccsesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccsesLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
           return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }

        public void Insert(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Set<T>().Update(t);
            _context.SaveChanges();
        }
    }
}

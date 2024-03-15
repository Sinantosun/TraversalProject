using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsesLayer.Abstract
{
    public interface IGenericUowDal<T> where T : class
    {
        void Insert(T t);
        void Update(T t);
        T getByID(int id);
        void MultiUpdate(List<T> t);
        List<Account> ListAccounts();
    }
}

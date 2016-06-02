using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.DataAccess
{
   public interface IDBStore<T>
    {
       void Add(T newEntity);

       void Update(T entity);

       void Remove(T entity);

       IQueryable<T> Query();

       IQueryable<T> FindWhere(Expression<Func<T, bool>> predicate);

       T FindFirstWhere(Expression<Func<T, bool>> predicate);
    }
}

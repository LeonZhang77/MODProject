using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.MODLibrarySystem.DataAccess
{
    public class DBStore<T> : IDBStore<T> where T : class
    {
        private readonly DbSet<T> querySet;
        private readonly DBSource context;

        public DBStore(DBSource entities)
        {
            this.context = entities;
            querySet = entities.GetDbSet<T>();
        }

        public void Add(T newEntity)
        {
            querySet.Add(newEntity);
        }

        public void Update(T entity)
        {
            querySet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            querySet.Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return querySet.AsQueryable<T>();
        }

        public IQueryable<T> FindWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return querySet.Where(predicate).AsQueryable<T>();
        }

        public T FindFirstWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return querySet.Where(predicate).FirstOrDefault<T>();
        }       
    }
}

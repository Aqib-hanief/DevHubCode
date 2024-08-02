using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        DevHubDBEntities db = new DevHubDBEntities();
        public void Delete(T obj)
        {
            db.Set<T>().Remove(obj);
        }

        public IQueryable<T> FindByQueryable(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAllQueryable()
        {
            return db.Set<T>().AsQueryable();
        }

        public T GetById(Guid Id)
        {
            return db.Set<T>().Find(Id);
        }

        public void Insert(T obj)
        {
            db.Set<T>().Add(obj);
        }

        public int Save()
        {
            return db.SaveChanges();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void AddRange(List<T> objs)
        {
            db.Set<T>().AddRange(objs);
        }

        public void DeleteRange(List<T> objs)
        {
            db.Set<T>().RemoveRange(objs);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAllQueryable();

        IQueryable<T> FindByQueryable(Expression<Func<T, bool>> predicate);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);

        int Save();

        T GetById(Guid Id);

        void AddRange(List<T> objs);

        void DeleteRange(List<T> objs);




    }
}

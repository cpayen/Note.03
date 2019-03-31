using Note.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Note.Core.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<T> FindAsync(Guid id);
        Task<T> FindAsync(Guid id, params string[] includes);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Attach(T entity);
    }
}

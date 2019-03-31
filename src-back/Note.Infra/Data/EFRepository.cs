using Microsoft.EntityFrameworkCore;
using Note.Core.Data;
using Note.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Note.Infra.Data.SQLServer
{
    public class EFRepository<T> : IRepository<T> where T : Entity
    {
        private readonly EFContext _context;
        private DbSet<T> _entities;

        public EFRepository(EFContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> FindAsync(Guid id, params string[] includes)
        {
            var query = _entities;
            foreach (var include in includes)
            {
                query.Include(include);
            }
            return await query.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _entities;
            foreach (var include in includes)
            {
                query.Include(include);
            }
            return await query.Where(predicate).ToListAsync();
        }

        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Update(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Attach(entity);
            return entity;
        }

        //public async Task<T> FindAsync(Guid id, params Expression<Func<T, object>>[] includes)
        //{
        //    var query = _entities.Where(s => s.Id == id);
        //    return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).FirstOrDefaultAsync();
        //}
    }
}

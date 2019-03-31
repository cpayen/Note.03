using Note.Core.Data;
using Note.Core.Entities.Base;
using Note.Infra.Data.SQLServer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Infra.Data
{
    public class EFEntityFactory : IEntityFactory
    {
        private readonly IDictionary<string, object> _repositories;
        private EFContext _context;

        public EFEntityFactory(EFContext context)
        {
            _repositories = new Dictionary<string, object>();
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : Entity
        {
            string key = typeof(T).Name;

            if (_repositories.ContainsKey(key))
            {
                return (IRepository<T>)_repositories[key];
            }

            IRepository<T> repository = new EFRepository<T>(_context);
            _repositories.Add(key, repository);
            return repository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

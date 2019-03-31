using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Columns
    {
        protected readonly IEntityFactory _factory;

        public Columns(IEntityFactory factory)
        {
            _factory = factory;
        }

        public async Task<Column> CreateAsync(Guid dashboardId, string name, string description, int? position)
        {
            var dashboard = await _factory.GetRepository<Dashboard>().FindAsync(dashboardId, "Columns");

            if (dashboard == null)
            {
                throw new NotFoundException($"Dashboard [{dashboardId}] entity not found.");
            }

            var entity = new Column
            {
                Name = name,
                Description = description,
                Dashboard = dashboard
            };

            if(position.HasValue)
            {
                entity.Order = position.Value;
                foreach (var column in dashboard.Columns.Where(o => o.Order >= position))
                {
                    column.Order++;
                    _factory.GetRepository<Column>().Update(column);
                }
            }
            else
            {
                entity.Order = dashboard.Columns.Count;
            }

            _factory.GetRepository<Column>().Create(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Column> UpdateAsync(Guid id, string name, string description)
        {
            var entity = await _factory.GetRepository<Column>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Column [{id}] entity not found.");
            }

            entity.Name = name;
            entity.Description = description;

            _factory.GetRepository<Column>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Column> MoveAsync(Guid id, int position)
        {
            var entity = await _factory.GetRepository<Column>().FindAsync(id, "Dashboard.Columns");

            if (entity == null)
            {
                throw new NotFoundException($"Column [{id}] entity not found.");
            }
            
            if(position > entity.Dashboard.Columns.Count - 1)
            {
                position = entity.Dashboard.Columns.Count - 1;
            }

            var oldPosition = entity.Order;
            var newPosition = position;

            entity.Order = position;
            _factory.GetRepository<Column>().Update(entity);

            if (newPosition > oldPosition)
            {
                foreach (var column in entity.Dashboard.Columns.Where(o => o.Order > oldPosition && o.Order <= newPosition))
                {
                    column.Order--;
                    _factory.GetRepository<Column>().Update(column);
                }
            }
            else
            {
                foreach (var column in entity.Dashboard.Columns.Where(o => o.Order >= newPosition && o.Order < oldPosition))
                {
                    column.Order++;
                    _factory.GetRepository<Column>().Update(column);
                }
            }

            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Column> DeleteAsync(Guid id)
        {
            var entity = await _factory.GetRepository<Column>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Column [{id}] entity not found.");
            }

            _factory.GetRepository<Column>().Delete(entity);
            await _factory.SaveAsync();

            return entity;
        }

        //TODO: Handle items?
    }
}

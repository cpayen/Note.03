using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Categories
    {
        protected readonly IEntityFactory _factory;

        public Categories(IEntityFactory factory)
        {
            _factory = factory;
        }

        public async Task<Category> CreateAsync(Guid dashboardId, string name, string color)
        {
            var dashboard = await _factory.GetRepository<Dashboard>().FindAsync(dashboardId);

            if (dashboard == null)
            {
                throw new NotFoundException($"Dashboard [{dashboardId}] entity not found.");
            }

            var entity = new Category
            {
                Name = name,
                Color = color,
                Dashboard = dashboard
            };

            _factory.GetRepository<Category>().Create(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Category> UpdateAsync(Guid id, string name, string color)
        {
            var entity = await _factory.GetRepository<Category>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Category [{id}] entity not found.");
            }

            entity.Name = name;
            entity.Color = color;

            _factory.GetRepository<Category>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            var entity = await _factory.GetRepository<Category>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Category [{id}] entity not found.");
            }

            foreach (var itemCategory in await _factory.GetRepository<ItemCategory>().FindByAsync(o => o.Category.Id == entity.Id))
            {
                _factory.GetRepository<ItemCategory>().Delete(itemCategory);
            }

            _factory.GetRepository<Category>().Delete(entity);
            await _factory.SaveAsync();

            return entity;
        }
    }
}

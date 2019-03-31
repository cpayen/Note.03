using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using Note.Core.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Dashboards
    {
        protected readonly IEntityFactory _factory;
        protected readonly ICurrentUserInfos _currentUserInfos;

        public Dashboards(IEntityFactory factory, ICurrentUserInfos currentUserInfos)
        {
            _factory = factory;
            _currentUserInfos = currentUserInfos;
        }

        public async Task<IEnumerable<Dashboard>> GetAllAsync()
        {
            var entities = await _factory.GetRepository<Dashboard>().GetAllAsync();
            return entities;
        }

        public async Task<IEnumerable<Dashboard>> GetForUserAsync(Guid userId)
        {
            var entities = await _factory.GetRepository<Dashboard>().FindByAsync(o => o.Owner.Id == userId || o.Public);
            return entities;
        }

        public async Task<Dashboard> FindAsync(Guid id)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id, "Columns.Items.ItemCategories.Category");

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }

            return entity;
        }

        public async Task<Dashboard> CreateAsync(Guid? ownerId, string name, string description, bool isPublic)
        {
            var owner = await _factory.GetRepository<AppUser>().FindAsync(ownerId ?? Guid.Parse(_currentUserInfos.Id));

            if (owner == null)
            {
                throw new NotFoundException($"AppUser [{ownerId}] entity not found.");
            }

            var entity = new Dashboard
            {
                Name = name,
                Description = description,
                Public = isPublic,
                Archived = false,
                Owner = owner
            };

            _factory.GetRepository<Dashboard>().Create(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Dashboard> UpdateAsync(Guid id, string name, string description, bool? isPublic)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }

            entity.Name = name;
            entity.Description = description;

            if(isPublic.HasValue)
            {
                entity.Public = isPublic.Value;
            }

            _factory.GetRepository<Dashboard>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Dashboard> ChangeOwnerAsync(Guid id, Guid ownerId)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }
            
            var owner = await _factory.GetRepository<AppUser>().FindAsync(ownerId);

            if (owner == null)
            {
                throw new NotFoundException($"AppUser [{ownerId}] entity not found.");
            }

            entity.Owner = owner;

            _factory.GetRepository<Dashboard>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Dashboard> DeleteAsync(Guid id)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }

            _factory.GetRepository<Dashboard>().Delete(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Dashboard> SetVisibilityAsync(Guid id, bool isPublic)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }

            entity.Public = isPublic;

            _factory.GetRepository<Dashboard>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Dashboard> SetStatusAsync(Guid id, bool isArchived)
        {
            var entity = await _factory.GetRepository<Dashboard>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Dashboard [{id}] entity not found.");
            }

            entity.Archived = isArchived;

            _factory.GetRepository<Dashboard>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        //TODO: Handle columns?
        //TODO: Handle categories?
    }
}

using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class AppUsers
    {
        protected readonly IEntityFactory _factory;

        public AppUsers(IEntityFactory factory)
        {
            _factory = factory;
        }
        
        public async Task<AppUser> LoginAsync(string login, string password)
        {
            var users = await _factory.GetRepository<AppUser>().FindByAsync(o => o.UserName == login);
            var user = users.FirstOrDefault();

            if (user != null)
            {
                if (user.CheckPassword(password))
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            var entities = await _factory.GetRepository<AppUser>().GetAllAsync();
            return entities;
        }

        public async Task<AppUser> FindAsync(Guid id)
        {
            var entity = await _factory.GetRepository<AppUser>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"AppUser [{id}] entity not found.");
            }

            return entity;
        }

        public async Task<bool> UserNameAvailable(string userName)
        {
            var users = await _factory.GetRepository<AppUser>().FindByAsync(o => o.UserName == userName);
            return !users.Any();
        }

        public async Task<AppUser> CreateAsync(string userName, string firstName, string lastName, string email, string password, IEnumerable<string> roles)
        {
            if (!await UserNameAvailable(userName))
            {
                throw new NotSupportedException("UserName already exists.");
            }

            var entity = new AppUser
            {
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            
            entity.SetPassword(password);
            entity.SetRoles(roles);
            
            _factory.GetRepository<AppUser>().Create(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<AppUser> UpdateAsync(Guid id, string firstName, string lastName, string email, IEnumerable<string> roles)
        {
            var entity = await _factory.GetRepository<AppUser>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"AppUser [{id}] entity not found.");
            }

            entity.FirstName = firstName;
            entity.LastName = lastName;
            entity.Email = email;
            entity.SetRoles(roles);

            _factory.GetRepository<AppUser>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<AppUser> ChangePasswordAsync(Guid id, string password)
        {
            var entity = await _factory.GetRepository<AppUser>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"AppUser [{id}] entity not found.");
            }

            entity.SetPassword(password);

            _factory.GetRepository<AppUser>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<AppUser> DeleteAsync(Guid id)
        {
            var entity = await _factory.GetRepository<AppUser>().FindAsync(id);

            if(entity == null)
            {
                throw new NotFoundException($"AppUser [{id}] entity not found.");
            }

            _factory.GetRepository<AppUser>().Delete(entity);
            await _factory.SaveAsync();

            return entity;
        }
    }
}

using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Core.Services
{
    public class Items
    {

        protected readonly IEntityFactory _factory;

        public Items(IEntityFactory factory)
        {
            _factory = factory;
        }

        public async Task<Item> CreateAsync(Guid columnId, string name, string description, int? position)
        {
            var column = await _factory.GetRepository<Column>().FindAsync(columnId, "Items");

            if (column == null)
            {
                throw new NotFoundException($"Column [{columnId}] entity not found.");
            }

            var entity = new Item
            {
                Name = name,
                Description = description,
                Column = column,
                Priority = Priority.Low,
                Complete = false
            };

            if (position.HasValue)
            {
                if (position.Value > column.Items.Count)
                {
                    position = column.Items.Count;
                }

                entity.Order = position.Value;
                foreach (var item in column.Items.Where(o => o.Order >= position))
                {
                    item.Order++;
                    _factory.GetRepository<Item>().Update(item);
                }
            }
            else
            {
                entity.Order = column.Items.Count;
            }

            _factory.GetRepository<Item>().Create(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Item> UpdateAsync(Guid id, string name, string description, bool complete)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            entity.Name = name;
            entity.Description = description;
            entity.Complete = complete;

            _factory.GetRepository<Item>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Item> MoveAsync(Guid id, Guid? columnId, int? position)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id, "Column.Items");

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            // Change column
            if(columnId.HasValue && columnId.Value != entity.Column.Id)
            {
                var oldColumn = entity.Column;
                var newColumn = await _factory.GetRepository<Column>().FindAsync(columnId.Value);

                if (newColumn == null)
                {
                    throw new NotFoundException($"Column [{columnId.Value}] entity not found.");
                }

                // Old column items
                foreach (var item in oldColumn.Items.Where(o => o.Order >= entity.Order - 1))
                {
                    item.Order--;
                    _factory.GetRepository<Item>().Update(item);
                }

                // New column items
                var newPosition = position.HasValue && position.Value <= newColumn.Items.Count() ? position.Value : newColumn.Items.Count();
                foreach (var item in newColumn.Items.Where(o => o.Order >= newPosition))
                {
                    item.Order++;
                    _factory.GetRepository<Item>().Update(item);
                }

                // Update item
                entity.Column = newColumn;
                entity.Order = newPosition;
                _factory.GetRepository<Item>().Update(entity);

                // Save changes
                await _factory.SaveAsync();
            }

            // Change position
            else
            {
                if (position.HasValue && position.Value != entity.Order)
                {
                    if (position.Value > entity.Column.Items.Count - 1)
                    {
                        position = entity.Column.Items.Count - 1;
                    }

                    var column = entity.Column;
                    var oldPosition = entity.Order;
                    var newPosition = position.Value;

                    // Reorder other items
                    if (newPosition > oldPosition)
                    {
                        foreach (var item in column.Items.Where(o => o.Order > oldPosition && o.Order <= newPosition))
                        {
                            item.Order--;
                            _factory.GetRepository<Item>().Update(item);
                        }
                    }
                    else
                    {
                        foreach (var item in column.Items.Where(o => o.Order >= newPosition && o.Order < oldPosition))
                        {
                            item.Order++;
                            _factory.GetRepository<Item>().Update(item);
                        }
                    }

                    // Update item
                    entity.Order = newPosition;
                    _factory.GetRepository<Item>().Update(entity);

                    // Save changes
                    await _factory.SaveAsync();
                }
            }

            return entity;
        }

        public async Task<Item> DeleteAsync(Guid id)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            _factory.GetRepository<Item>().Delete(entity);
            await _factory.SaveAsync();

            return entity;
        }

        #region Complete status

        public async Task<Item> SetStatusAsync(Guid id, bool isComplete)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            entity.Complete = isComplete;
            _factory.GetRepository<Item>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        #endregion

        #region Priority

        public async Task<Item> SetPriorityAsync(Guid id, Priority priority)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            entity.Priority = priority;
            _factory.GetRepository<Item>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        #endregion

        #region Categories

        public async Task<Item> AddCategoryAsync(Guid id, Guid categoryId)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);
            
            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            var category = await _factory.GetRepository<Category>().FindAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] entity not found.");
            }

            entity.AddCategory(category);
            _factory.GetRepository<Item>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        public async Task<Item> RemoveCategoryAsync(Guid id, Guid categoryId)
        {
            var entity = await _factory.GetRepository<Item>().FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"Item [{id}] entity not found.");
            }

            var category = await _factory.GetRepository<Category>().FindAsync(categoryId);

            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] entity not found.");
            }

            entity.RemoveCategory(category);
            _factory.GetRepository<Item>().Update(entity);
            await _factory.SaveAsync();

            return entity;
        }

        #endregion
    }
}

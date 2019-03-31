using Note.Api.DTO.AppUser;
using Note.Api.DTO.Category;
using Note.Api.DTO.Column;
using Note.Api.DTO.Dashboard;
using Note.Api.DTO.Item;
using Note.Api.DTO.Login;
using Note.Core.Entities;
using System.Collections.Generic;

namespace Note.Api.DTO
{
    public class Mappers
    {
        #region AppUser

        public static AuthenticatedUserDTO GetAuthenticatedUserDTO(Core.Entities.AppUser entity)
        {
            return new AuthenticatedUserDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                FullName = entity.GetFullName(),
                Email = entity.Email,
                Roles = entity.GetRoles()
            };
        }

        public static AppUserDTO GetAppUserDTO(Core.Entities.AppUser entity)
        {
            return new AppUserDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                FullName = entity.GetFullName(),
                Email = entity.Email,
                Roles = entity.GetRoles(),
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        public static AppUserLightDTO GetAppUserLightDTO(Core.Entities.AppUser entity)
        {
            return new AppUserLightDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                FullName = entity.GetFullName(),
                Email = entity.Email
            };
        }

        #endregion

        #region Category

        public static CategoryDTO GetCategoryDTO(Core.Entities.Category entity)
        {
            return new CategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Color = entity.Color,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };
        }

        #endregion

        #region Item

        public static ItemDTO GetItemDTO(Core.Entities.Item entity)
        {
            var dto = new ItemDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Order = entity.Order,
                Complete = entity.Complete,
                Priority = entity.Priority,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };

            dto.Categories = new List<CategoryDTO>();
            foreach (var category in entity.GetCategories())
            {
                dto.Categories.Add(GetCategoryDTO(category));
            }

            return dto;
        }

        #endregion

        #region Column

        public static ColumnDTO GetColumnDTO(Core.Entities.Column entity)
        {
            var dto = new ColumnDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Order = entity.Order,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy
            };

            dto.Items = new List<ItemDTO>();
            foreach (var item in entity.GetItems())
            {
                dto.Items.Add(GetItemDTO(item));
            }

            return dto;
        }

        #endregion

        #region Dashboard

        public static DashboardLightDTO GetDashboardLightDTO(Core.Entities.Dashboard entity)
        {
            return new DashboardLightDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Public = entity.Public,
                Archived = entity.Archived,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy,
                Owner = GetAppUserLightDTO(entity.Owner)
            };
        }

        public static DashboardDTO GetDashboardDTO(Core.Entities.Dashboard entity)
        {
            var dto = new DashboardDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Public = entity.Public,
                Archived = entity.Archived,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedAt = entity.UpdatedAt,
                UpdatedBy = entity.UpdatedBy,
                Owner = GetAppUserLightDTO(entity.Owner),
            };

            dto.Columns = new List<ColumnDTO>();
            foreach (var column in entity.GetColumns())
            {
                dto.Columns.Add(GetColumnDTO(column));
            }

            return dto;
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.DTO;
using Note.Api.DTO.Category;
using Note.Core.Services;
using System;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/dashboards/{dashboardId}/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        protected readonly Categories _categories;

        public CategoriesController(Categories categories)
        {
            _categories = categories;
        }

        // POST api/v1/dashboards/123/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostAsync(Guid dashboardId, [FromBody] CreateCategoryDTO dto)
        {
            var item = await _categories.CreateAsync(dashboardId, dto.Name, dto.Color);
            return Created($"api/v1/dashboards/{dashboardId}/categories/{item.Id}", Mappers.GetCategoryDTO(item));
        }

        // PUT api/v1/dashboards/123/categories/123
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> PutAsync(Guid dashboardId, Guid id, [FromBody] UpdateCategoryDTO dto)
        {
            var item = await _categories.UpdateAsync(id, dto.Name, dto.Color);
            return Ok(Mappers.GetCategoryDTO(item));
        }

        // DELETE api/v1/dashboards/123/categories/123
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> DeleteAsync(Guid dashboardId, Guid id)
        {
            var item = await _categories.DeleteAsync(id);
            return Ok(Mappers.GetCategoryDTO(item));
        }
    }
}
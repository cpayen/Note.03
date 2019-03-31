using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.DTO;
using Note.Api.DTO.Item;
using Note.Core.Services;
using System;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/dashboards/{dashboardId}/columns/{columnId}/items")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        protected readonly Items _items;

        public ItemsController(Items items)
        {
            _items = items;
        }

        // POST api/v1/dashboards/123/columns/123/items
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostAsync(Guid dashboardId, Guid columnId, [FromBody] CreateItemDTO dto)
        {
            var item = await _items.CreateAsync(columnId, dto.Name, dto.Description, dto.Position);
            return Created($"api/v1/dashboards/{dashboardId}/columns/{columnId}/items/{item.Id}", Mappers.GetItemDTO(item));
        }

        // PUT api/v1/dashboards/123/columns/123/items/123
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> PutAsync(Guid dashboardId, Guid columnId, Guid id, [FromBody] UpdateItemDTO dto)
        {
            var item = await _items.UpdateAsync(id, dto.Name, dto.Description, dto.Complete);
            return Ok(Mappers.GetItemDTO(item));
        }

        // DELETE api/v1/dashboards/123/columns/123/items/123
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid dashboardId, Guid columnId, Guid id)
        {
            var item = await _items.DeleteAsync(id);
            return Ok(Mappers.GetItemDTO(item));
        }

        // PATCH api/v1/dashboards/123/columns/123/items/123/move
        [HttpPatch("{id}/move")]
        public async Task<ActionResult> MoveAsync(Guid dashboardId, Guid columnId, Guid id, [FromBody] MoveItemDTO dto)
        {
            var item = await _items.MoveAsync(id, dto.ColumnId, dto.Position);
            return Ok(Mappers.GetItemDTO(item));
        }

        // PATCH api/v1/dashboards/123/columns/123/items/123/priority
        [HttpPatch("{id}/priority")]
        public async Task<ActionResult> SetPriorityAsync(Guid dashboardId, Guid columnId, Guid id, [FromBody] ChangeItemPriorityDTO dto)
        {
            var item = await _items.SetPriorityAsync(id, dto.Priority);
            return Ok(Mappers.GetItemDTO(item));
        }

        // PATCH api/v1/dashboards/123/columns/123/items/123/complete
        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> SetStatusAsync(Guid dashboardId, Guid columnId, Guid id, [FromBody] ChangeItemStatusDTO dto)
        {
            var item = await _items.SetStatusAsync(id, dto.IsComplete);
            return Ok(Mappers.GetItemDTO(item));
        }

    }
}
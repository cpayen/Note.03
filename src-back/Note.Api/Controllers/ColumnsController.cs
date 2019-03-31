using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.DTO;
using Note.Api.DTO.Column;
using Note.Core.Services;
using System;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/dashboards/{dashboardId}/columns")]
    [ApiController]
    [Authorize]
    public class ColumnsController : ControllerBase
    {
        protected readonly Columns _columns;

        public ColumnsController(Columns columns)
        {
            _columns = columns;
        }

        // POST api/v1/dashboards/123/columns
        [HttpPost]
        public async Task<ActionResult<ColumnDTO>> PostAsync(Guid dashboardId, [FromBody] CreateColumnDTO dto)
        {
            var item = await _columns.CreateAsync(dashboardId, dto.Name, dto.Description, dto.Position);
            return Created($"api/v1/dashboards/{dashboardId}/columns/{item.Id}", Mappers.GetColumnDTO(item));
        }

        // PUT api/v1/dashboards/123/columns/123
        [HttpPut("{id}")]
        public async Task<ActionResult<ColumnDTO>> PutAsync(Guid dashboardId, Guid id, [FromBody] UpdateColumnDTO dto)
        {
            var item = await _columns.UpdateAsync(id, dto.Name, dto.Description);
            return Ok(Mappers.GetColumnDTO(item));
        }

        // DELETE api/v1/dashboards/123/columns/123
        [HttpDelete("{id}")]
        public async Task<ActionResult<ColumnDTO>> DeleteAsync(Guid dashboardId, Guid id)
        {
            var item = await _columns.DeleteAsync(id);
            return Ok(Mappers.GetColumnDTO(item));
        }

        // PATCH api/v1/dashboards/123/columns/123/move
        [HttpPatch("{id}/move")]
        public async Task<ActionResult> MoveAsync(Guid dashboardId, Guid id, [FromBody] MoveColumnDTO dto)
        {
            var item = await _columns.MoveAsync(id, dto.Position);
            return Ok(Mappers.GetColumnDTO(item));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.DTO;
using Note.Api.DTO.Dashboard;
using Note.Core.Identity;
using Note.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/dashboards")]
    [ApiController]
    [Authorize]
    public class DashboardsController : ControllerBase
    {
        protected readonly Dashboards _dashboards;
        protected readonly ICurrentUserInfos _currentUserInfo;

        public DashboardsController(Dashboards dashboards, ICurrentUserInfos currentUserInfo)
        {
            _dashboards = dashboards;
            _currentUserInfo = currentUserInfo;
        }

        // GET api/v1/dashboards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DashboardLightDTO>>> GetAsync()
        {
            var items = await _dashboards.GetAllAsync();
            var allowedItems = items.Where(o => o.Public || o.Owner.Id == Guid.Parse(_currentUserInfo.Id) || _currentUserInfo.HasRole(UserRoles.AppAdmin));
            return Ok(items.Select(o => Mappers.GetDashboardLightDTO(o)));
        }

        // GET api/v1/dashboards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DashboardDTO>> FindAsync(Guid id)
        {
            var item = await _dashboards.FindAsync(id);
            return Ok(Mappers.GetDashboardDTO(item));
        }

        // POST api/v1/dashboards
        [HttpPost]
        public async Task<ActionResult<DashboardLightDTO>> PostAsync([FromBody] CreateDashboardDTO dto)
        {
            var item = await _dashboards.CreateAsync(dto.OwnerId, dto.Name, dto.Description, dto.Public);
            return Created($"api/v1/dashboards/{item.Id}", Mappers.GetDashboardLightDTO(item));
        }

        // PUT api/v1/dashboards/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DashboardLightDTO>> PutAsync(Guid id, [FromBody] UpdateDashboardDTO dto)
        {
            var item = await _dashboards.UpdateAsync(id, dto.Name, dto.Description, dto.Public);
            return Ok(Mappers.GetDashboardLightDTO(item));
        }

        // DELETE api/v1/dashboards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _dashboards.DeleteAsync(id);
            return Ok();
        }

        // PATCH api/v1/dashboards/5/change-owner
        [HttpPatch("{id}/change-owner")]
        public async Task<ActionResult<DashboardLightDTO>> ChangeOwnerAsync(Guid id, [FromBody] ChangeDashboardOwnerDTO dto)
        {
            var item = await _dashboards.ChangeOwnerAsync(id, dto.OwnerId);
            return Ok(Mappers.GetDashboardLightDTO(item));
        }

        // PUT api/v1/dashboards/5/visibility
        [HttpPatch("{id}/visible")]
        public async Task<ActionResult<DashboardLightDTO>> SetVisibilityAsync(Guid id, [FromBody] ChangeDashboardVisibilityDTO dto)
        {
            var item = await _dashboards.SetVisibilityAsync(id, dto.IsPublic);
            return Ok(Mappers.GetDashboardLightDTO(item));
        }

        // PUT api/v1/dashboards/5/status
        [HttpPatch("{id}/archived")]
        public async Task<ActionResult<DashboardLightDTO>> SetStatusAsync(Guid id, [FromBody] ChangeDashboardStatusDTO dto)
        {
            var item = await _dashboards.SetStatusAsync(id, dto.IsArchived);
            return Ok(Mappers.GetDashboardLightDTO(item));
        }
    }
}
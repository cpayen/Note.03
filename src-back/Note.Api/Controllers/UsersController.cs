using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.Api.DTO;
using Note.Api.DTO.AppUser;
using Note.Core.Identity;
using Note.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        protected readonly AppUsers _appUsers;

        public UsersController(AppUsers appUsers)
        {
            _appUsers = appUsers;
        }

        // GET api/users
        [HttpGet]
        [Authorize(Roles = UserRoles.AppAdmin)]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAsync()
        {
            var items = await _appUsers.GetAllAsync();
            return Ok(items.Select(o => Mappers.GetAppUserDTO(o)).ToList());
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAsync(Guid id)
        {
            var item = await _appUsers.FindAsync(id);
            return Ok(Mappers.GetAppUserDTO(item));
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> PostAsync([FromBody] CreateAppUserDTO dto)
        {
            var item = await _appUsers.CreateAsync(dto.UserName, dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.Roles);
            return Ok(Mappers.GetAppUserDTO(item));
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUserDTO>> PutAsync(Guid id, [FromBody] UpdateAppUserDTO dto)
        {
            var item = await _appUsers.UpdateAsync(id, dto.FirstName, dto.LastName, dto.Email, dto.Roles);
            return Ok(Mappers.GetAppUserDTO(item));
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.AppAdmin)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var item = await _appUsers.DeleteAsync(id);
            return Ok(Mappers.GetAppUserDTO(item));
        }

        // PUT api/users/5
        [HttpPut("{id}/change-password")]
        public async Task<ActionResult<AppUserDTO>> ChangePasswordAsync(Guid id, [FromBody] ChangeAppUserPasswordDTO dto)
        {
            var item = await _appUsers.ChangePasswordAsync(id, dto.Password);
            return Ok(Mappers.GetAppUserDTO(item));
        }
    }
}
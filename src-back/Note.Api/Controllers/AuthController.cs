using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Note.Api.DTO;
using Note.Api.DTO.Login;
using Note.Core.Services;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Note.Api.Controllers
{
    //TODO: Add token expiration, token refresh...

    [Route("api/auth")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        protected readonly AppUsers _appUsers;
        protected readonly IConfiguration _configuration;

        public AuthController(AppUsers appUsers, IConfiguration configuration)
        {
            _appUsers = appUsers;
            _configuration = configuration;
        }

        /// <summary>
        /// Try to log a user in.
        /// </summary>
        /// <param name="dto">LoginDTO with username and password.</param>
        /// <returns>An AuthenticatedUserDTO containing a token if authentication succeeded, unauthorized response otherwise.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<AuthenticatedUserDTO>> LoginAsync([FromBody] LoginDTO dto)
        {
            var user = await _appUsers.LoginAsync(dto.UserName, dto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var loggedInUserDTO = Mappers.GetAuthenticatedUserDTO(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUserDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, loggedInUserDTO.UserName),
                new Claim(ClaimTypes.Email, loggedInUserDTO.Email)
            };

            foreach (var role in loggedInUserDTO.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if(!string.IsNullOrEmpty(loggedInUserDTO.FirstName))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, loggedInUserDTO.FirstName));
            }

            if (!string.IsNullOrEmpty(loggedInUserDTO.LastName))
            {
                claims.Add(new Claim(ClaimTypes.Surname, loggedInUserDTO.LastName));
            }

            loggedInUserDTO.Token = BuildToken(claims);

            return Ok(loggedInUserDTO);
        }

        private string BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
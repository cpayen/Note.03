using Microsoft.AspNetCore.Http;
using Note.Core.Identity;
using System.Linq;
using System.Security.Claims;

namespace Note.Api.Identity
{
    public class CurrentWebUserInfos : ICurrentUserInfos
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string AnonymousUserName = "Anonymous";

        public CurrentWebUserInfos(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        private ClaimsPrincipal _currentUserClaims;
        private ClaimsPrincipal _claims
        {
            get
            {
                if(_currentUserClaims == null)
                {
                    _currentUserClaims = _httpContextAccessor.HttpContext.User;
                }
                return _currentUserClaims;
            }
        }
        
        public string Id
        {
            get
            {
                return _claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public string UserName
        {
            get
            {
                return _claims.Identity.Name ?? AnonymousUserName;
            }
        }

        public string FullName
        {
            get
            {
                return 
                    _claims.FindFirst(ClaimTypes.GivenName) != null && _claims.FindFirst(ClaimTypes.Surname) != null ? 
                    $"{_claims.FindFirst(ClaimTypes.GivenName) } {_claims.FindFirst(ClaimTypes.Surname)}" :
                    UserName;
            }
        }

        public bool HasRole(string role)
        {
            var roles = _claims.FindFirst(ClaimTypes.Role).ToString().Split(',');
            return roles.Contains(role);
        }
    }
}

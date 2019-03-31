using System;
using System.Collections.Generic;

namespace Note.Api.DTO.Login
{
    public class AuthenticatedUserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
        
        public string Token { get; set; }
    }
}

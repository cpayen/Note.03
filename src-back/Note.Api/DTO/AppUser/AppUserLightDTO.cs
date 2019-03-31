using System;

namespace Note.Api.DTO.AppUser
{
    public class AppUserLightDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}

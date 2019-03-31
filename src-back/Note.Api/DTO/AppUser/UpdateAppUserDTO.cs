using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.AppUser
{
    public class UpdateAppUserDTO
    {
        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}

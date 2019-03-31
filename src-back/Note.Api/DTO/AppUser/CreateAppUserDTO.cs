using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.AppUser
{
    public class CreateAppUserDTO
    {
        [Required, MaxLength(100), RegularExpression("^[a-zA-Z0-9]+$")]
        public string UserName { get; set; }

        [MaxLength(250)]
        public string FirstName { get; set; }

        [MaxLength(250)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public IEnumerable<string> Roles { get; set; }
    }
}

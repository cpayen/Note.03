using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.AppUser
{
    public class ChangeAppUserPasswordDTO
    {
        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Dashboard
{
    public class ChangeDashboardVisibilityDTO
    {
        [Required]
        public bool IsPublic { get; set; }
    }
}

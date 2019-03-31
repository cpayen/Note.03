using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Dashboard
{
    public class ChangeDashboardStatusDTO
    {
        [Required]
        public bool IsArchived { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Item
{
    public class ChangeItemStatusDTO
    {
        [Required]
        public bool IsComplete { get; set; }
    }
}

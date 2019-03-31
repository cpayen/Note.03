using Note.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Item
{
    public class ChangeItemPriorityDTO
    {
        [Required]
        public Priority Priority { get; set; }
    }
}

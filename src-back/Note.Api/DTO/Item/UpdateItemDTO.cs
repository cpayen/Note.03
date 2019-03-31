using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Item
{
    public class UpdateItemDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Complete { get; set; }
    }
}

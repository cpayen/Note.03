using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Column
{
    public class UpdateColumnDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}

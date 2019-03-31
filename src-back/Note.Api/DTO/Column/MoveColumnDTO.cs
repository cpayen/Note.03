using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Column
{
    public class MoveColumnDTO
    {
        [Required]
        public int Position { get; set; }
    }
}

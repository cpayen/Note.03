using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Column
{
    public class CreateColumnDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// If not set, the column will be added at the last position.
        /// </summary>
        public int? Position { get; set; }
    }
}

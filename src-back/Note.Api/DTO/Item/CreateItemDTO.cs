using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Item
{
    public class CreateItemDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// If not set, the item will be added at the last position.
        /// </summary>
        public int? Position { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Category
{
    public class UpdateCategoryDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color format")]
        public string Color { get; set; }
    }
}

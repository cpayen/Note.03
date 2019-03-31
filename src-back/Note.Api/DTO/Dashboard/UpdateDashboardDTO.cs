using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Dashboard
{
    public class UpdateDashboardDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        /// <summary>
        /// If not set, wil not be changed.
        /// </summary>
        public bool? Public { get; set; }
    }
}

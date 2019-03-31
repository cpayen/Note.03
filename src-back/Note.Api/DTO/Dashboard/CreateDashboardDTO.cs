using System;
using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Dashboard
{
    public class CreateDashboardDTO
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Public { get; set; }

        /// <summary>
        /// If not set, current authenticated user will be set as the owner of the created dashboard.
        /// </summary>
        public Guid? OwnerId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Note.Api.DTO.Dashboard
{
    public class ChangeDashboardOwnerDTO
    {
        [Required]
        public Guid OwnerId { get; set; }
    }
}

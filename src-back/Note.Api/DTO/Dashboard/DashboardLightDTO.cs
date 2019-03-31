using Note.Api.DTO.AppUser;
using System;

namespace Note.Api.DTO.Dashboard
{
    public class DashboardLightDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public bool Archived { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public AppUserLightDTO Owner { get; set; }
    }
}

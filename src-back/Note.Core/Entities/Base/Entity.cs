using System;

namespace Note.Core.Entities.Base
{
    public class Entity : IIdentifiable, ITrackable
    {
        // IIdentifiable
        public Guid Id { get; set; }

        // ITrackable implementation (handled in EF context).
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}

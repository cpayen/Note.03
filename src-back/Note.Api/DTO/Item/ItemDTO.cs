using Note.Api.DTO.Category;
using Note.Core.Entities;
using System;
using System.Collections.Generic;

namespace Note.Api.DTO.Item
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool Complete { get; set; }
        public Priority Priority { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}

using Note.Api.DTO.Item;
using System;
using System.Collections.Generic;

namespace Note.Api.DTO.Column
{
    public class ColumnDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public List<ItemDTO> Items { get; set; }
    }
}

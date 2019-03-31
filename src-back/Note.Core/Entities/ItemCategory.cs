using Note.Core.Entities.Base;
using System;

namespace Note.Core.Entities
{
    public class ItemCategory : Entity
    {
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

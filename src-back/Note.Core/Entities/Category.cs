using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        
        public virtual Dashboard Dashboard { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }
}

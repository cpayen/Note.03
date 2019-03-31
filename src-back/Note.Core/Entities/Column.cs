using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class Column : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        
        public virtual Dashboard Dashboard { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}

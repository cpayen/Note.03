using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class Dashboard : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public bool Archived { get; set; }
        
        public virtual AppUser Owner { get; set; }

        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}

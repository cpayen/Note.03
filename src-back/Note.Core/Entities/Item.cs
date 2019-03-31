using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class Item : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Priority Priority { get; set; }
        public bool Complete { get; set; }

        public virtual Column Column { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }

    public enum Priority
    {
        Low = 0,
        Medium = 1,
        Hight = 2,
        Critical = 3
    }
}

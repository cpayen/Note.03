using Note.Core.Entities.Base;
using System.Collections.Generic;

namespace Note.Core.Entities
{
    public class AppUser : Entity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Dashboard> Dashboards { get; set; }
    }
}

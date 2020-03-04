using System;
using System.Collections.Generic;

namespace entities
{
    public partial class User : entities._base.EntityBase<Guid>
    {
        public User()
        {
            ChangeLog = new HashSet<ChangeLog>();
            UserRol = new HashSet<UserRol>();
        }

        public string Name { get; set; }

        public virtual ICollection<ChangeLog> ChangeLog { get; set; }
        public virtual ICollection<UserRol> UserRol { get; set; }
    }
}

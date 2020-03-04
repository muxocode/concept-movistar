using System;
using System.Collections.Generic;

namespace entities
{
    public partial class Rol : entities._base.EntityBase<Guid>
    {
        public Rol()
        {
            UserRol = new HashSet<UserRol>();
        }

        
        public string Name { get; set; }

        public virtual ICollection<UserRol> UserRol { get; set; }
    }
}

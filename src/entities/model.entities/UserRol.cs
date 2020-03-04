using System;
using System.Collections.Generic;

namespace entities
{
    public partial class UserRol: entities._base.EntityBase<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? RolId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual User User { get; set; }
    }
}

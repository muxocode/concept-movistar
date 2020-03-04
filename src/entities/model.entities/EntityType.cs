using System;
using System.Collections.Generic;

namespace entities
{
    public partial class EntityType : entities._base.EntityBase<Guid>
    {
        public EntityType()
        {
            ChangeLog = new HashSet<ChangeLog>();
        }

        
        public string Name { get; set; }

        public virtual ICollection<ChangeLog> ChangeLog { get; set; }
    }
}

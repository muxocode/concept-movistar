using System;
using System.Collections.Generic;

namespace entities
{
    public partial class ChangeLog : entities._base.EntityBase<Guid>
    {
        
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string PreviousValue { get; set; }
        public string CurrentValue { get; set; }
        public Guid EntityTypeId { get; set; }
        public Guid EntityId { get; set; }
        public Guid? TransactionBloq { get; set; }

        public virtual EntityType EntityType { get; set; }
        public virtual User User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace entities
{
    public partial class DataType: entities._base.EntityBase<Guid>
    {
        public DataType()
        {
            DetailTypeId = new HashSet<DetailTypeId>();
        }

        
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<DetailTypeId> DetailTypeId { get; set; }
    }
}

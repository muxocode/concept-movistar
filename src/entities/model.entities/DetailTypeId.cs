using System;
using System.Collections.Generic;

namespace entities
{
    public partial class DetailTypeId : entities._base.EntityBase<Guid>
    {
        public DetailTypeId()
        {
            OfferDetails = new HashSet<OfferDetails>();
        }

        
        public string Name { get; set; }
        public Guid DataTypeId { get; set; }

        public virtual DataType DataType { get; set; }
        public virtual ICollection<OfferDetails> OfferDetails { get; set; }
    }
}

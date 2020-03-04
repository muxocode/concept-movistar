using System;
using System.Collections.Generic;

namespace entities
{
    public partial class OfferType : entities._base.EntityBase<Guid>
    {
        public OfferType()
        {
            Offers = new HashSet<Offer>();
        }

        
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}

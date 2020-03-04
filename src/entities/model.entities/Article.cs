using System;
using System.Collections.Generic;

namespace entities
{
    public partial class Article: entities._base.EntityBase<Guid>
    {
        public Article()
        {
            OfferDetails = new HashSet<OfferDetails>();
        }

        
        public string Name { get; set; }

        public virtual ICollection<OfferDetails> OfferDetails { get; set; }
    }
}

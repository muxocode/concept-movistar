using System;
using System.Collections.Generic;

namespace entities
{
    public partial class OfferDetails : entities._base.EntityBase<Guid>
    {
        
        public Guid OfferId { get; set; }
        public Guid? ArticleId { get; set; }
        public Guid ObjectValueId { get; set; }
        public Guid DetailTypeId { get; set; }

        public virtual Article Article { get; set; }
        public virtual DetailTypeId DetailType { get; set; }
        //public virtual ObjectValue ObjectValue { get; set; }
        public virtual Offer Offer { get; set; }
    }
}

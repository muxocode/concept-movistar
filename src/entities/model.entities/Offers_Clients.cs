using System;
using System.Collections.Generic;

namespace entities
{
    public partial class OffersClients : entities._base.EntityBase<Guid>
    {
        public Guid OfferId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime Date { get; set; }
        public bool? Showed { get; set; }
        public bool? Visited { get; set; }
        public bool? Searched { get; set; }

        public virtual Client Client { get; set; }
        public virtual Offer Offer { get; set; }

    }
}

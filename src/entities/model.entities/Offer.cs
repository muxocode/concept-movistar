using System;
using System.Collections.Generic;

namespace entities
{
    public partial class Offer : entities._base.EntityBase<Guid>
    {
        public Offer()
        {
            OfferDetails = new HashSet<OfferDetails>();
            Offers_Clients = new HashSet<OffersClients>();
        }

        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Finish { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid OfferTypeId { get; set; }

        public virtual OfferType OfferType { get; set; }
        public virtual ICollection<OfferDetails> OfferDetails { get; set; }
        public virtual ICollection<OffersClients> Offers_Clients { get; set; }

    }
}

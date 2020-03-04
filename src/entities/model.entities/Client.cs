using System;
using System.Collections.Generic;

namespace entities
{
    public partial class Client : entities._base.EntityBase<Guid>
    {

        public Client()
        {
            Offers_Clients = new HashSet<OffersClients>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<OffersClients> Offers_Clients { get; set; }

    }
}

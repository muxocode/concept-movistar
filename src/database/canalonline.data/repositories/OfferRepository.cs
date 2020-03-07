using crossapp.unitOfWork;
using entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace canalonline.data
{
    /// <summary>
    /// Offer repository
    /// </summary>
    public class OfferRepository : _base.RepositoryBase<Offer>
    {
        public OfferRepository(IEntityUnitOfWork unitOfWork, movistarContext context) :base(unitOfWork, context)
        {
            
        }

        public override Task Add(Offer item)
        {
            /*
             * Aquí se añadiría el código especial para el prepositorio
             */

            return base.Add(item);
        }
    }
}

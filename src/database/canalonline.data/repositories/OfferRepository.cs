using crossapp.unitOfWork;
using entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace canalonline.data
{
    public class OfferRepository : _base.RepositoryBase<Offer>
    {
        public OfferRepository(IEntityUnitOfWork unitOfWork, movistarContext context) :base(unitOfWork, context)
        {

        }


    }
}

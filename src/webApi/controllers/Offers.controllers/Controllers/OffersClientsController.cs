using System;
using crossapp.log.logging;
using crossapp.services;
using crossapp.unitOfWork;
using entities;
using webApi.common.Controllers;

namespace Offers.webApi.Controllers
{

    public class OffersClientsController : ControllerBase<OffersClients, Guid>
    {
        //Declaramos explicitamente lo que permite nuestra api
        protected override bool Read => true;
        protected override bool Writte => true;

        public OffersClientsController(IService<OffersClients> service, IUnitOfWork unitOfWork, ILogHandler errorHandler) 
            :base(service, unitOfWork, errorHandler)
        {

        }
    }
}

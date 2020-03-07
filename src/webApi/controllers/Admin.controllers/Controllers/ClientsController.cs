using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crossapp.log.logging;
using crossapp.services;
using crossapp.unitOfWork;
using entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using webApi.common.Controllers;

namespace Admin.webApi.Controllers
{
    public class ClientsController : ControllerBase<Client, Guid>
    {
        private readonly IService<OffersClients> offerOffersClients;
        private readonly IService<Offer> OfferService;

        protected override bool Read => true;
        protected override bool Writte => true;

        public ClientsController(IService<Client> service, IService<Offer> offerService, IService<OffersClients> offerOffersClients, IUnitOfWork unitOfWork, ILogHandler errorHandler) 
            :base(service, unitOfWork, errorHandler)
        {
            OfferService = offerService;
            this.offerOffersClients = offerOffersClients;
        }


        [EnableQuery]
        [ODataRoute("Clients({key})/Offers")]
        public async Task<IActionResult> GetOffers([FromODataUri] Guid key)
        {
            try
            {

                //Son datos de prueba, como la BBDD se crea al vuelo, pues los IDs cambian
                var offerClients = (await offerOffersClients.Get()).ToList();
                var offers = (await OfferService.Get()).ToList();

                //En este punto aplicaríamos la lógica de negocio para saber las ofertas que le pertenecen al cliente
                //Sería interesante poder cruzarlo con lagñun paquete de negocio
                
                var result = from offer in offers
                             join oc in offerClients
                             on offer.Id equals oc.OfferId
                             select new
                             {
                                 Name= offer.Name,
                                 Price = offer.Price,
                                 Points = offer.Price / 10,
                                 Searched = oc.Searched,
                                 Showed = oc.Showed,
                                 Visited = oc.Visited,
                                 Date = oc.Date,
                                 Id = offer.Id
                             };

                return Ok(result.Take(30));
            }
            catch (Exception oEx)
            {
                return SendError("GetTranslations", oEx);
            }
        }
    }
}

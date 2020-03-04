using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using crossapp.file.logging;
using crossapp.services;
using crossapp.unitOfWork;
using entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Offers.webApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableQuery()]
    public class ClientsController : ControllerBase<Client, Guid>
    {
        private readonly IService<OffersClients> offerOffersClients;
        private readonly IService<Offer> OfferService;

        public ClientsController(IService<Client> service, IService<Offer> offerService, IService<OffersClients> offerOffersClients, IUnitOfWork unitOfWork, ILogHandler errorHandler) 
            :base(service, unitOfWork, errorHandler)
        {
            OfferService = offerService;
            this.offerOffersClients = offerOffersClients;
        }

        public class Temp
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public decimal points { get; set; }
            public bool? Searched { get; set; }
            public bool? Showed { get; set; }
            public bool? Visited { get; set; }
            public DateTime Date { get; set; }
            public Guid Id { get; set; }
        }

        [EnableQuery]
        [ODataRoute("Clients({key})/Offers")]
        public async Task<IActionResult> GetOffers([FromODataUri] Guid key)
        {
            try
            {
                var offerClients = (await offerOffersClients.Get()).ToList();
                var offers = (await OfferService.Get()).ToList();

                //En este punto aplicaríamos la lógica de negocio para saber las ofertas que le pertenecen al cliente
                //Sería interesante poder cruzarlo con lagñun paquete de negocio
                
                var result = from offer in offers
                             join oc in offerClients
                             on offer.Id equals oc.OfferId
                             where oc.ClientId == key
                             select new Temp
                             {
                                 Name= offer.Name,
                                 Price = offer.Price,
                                 points = offer.Price / 10,
                                 Searched = oc.Searched,
                                 Showed = oc.Showed,
                                 Visited = oc.Visited,
                                 Date = oc.Date,
                                 Id = offer.Id
                             };

                return Ok(result.AsQueryable());
            }
            catch (Exception oEx)
            {
                return SendError("GetTranslations", oEx);
            }
        }

        /*
        [HttpPost]
        [ODataRoute("Clients({key})/Offers({offerKey})/setState")]
        public async Task<IActionResult> Setstate([FromODataUri] Guid key, [FromODataUri] Guid offerKey, Delta<OffersClients> obj)
        {
            try
            {
                var item = new OffersClients
                {
                    OfferId=offerKey, 
                    ClientId=key,
                    Id=Guid.NewGuid()
                };

                obj.Patch(item);

                await offerOffersClients.Insert(item);

                this.UnitOfWork.SaveChanges();

                return Ok(item);
            }
            catch (Exception oEx)
            {
                return SendError("GetTranslations", oEx);
            }
        }
        */
    }
}

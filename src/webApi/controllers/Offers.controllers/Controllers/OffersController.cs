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
using movistar.model.bussines;
using webApi.common.Controllers;

namespace Offers.webApi.Controllers
{

    public class OffersController : ControllerBase<Offer, Guid>
    {
        private readonly IService<OfferType> OfferTypeService;
        private readonly IUserPreferencesManager Preferences;

        //Declaramos explicitamente lo que permite nuestra api
        protected override bool Read => true;

        public OffersController(IService<Offer> service, IService<OfferType> offerTypeService, IUnitOfWork unitOfWork, ILogHandler errorHandler, IUserPreferencesManager preferences) 
            :base(service, unitOfWork, errorHandler)
        {
            OfferTypeService = offerTypeService;
            Preferences = preferences;
        }

        [HttpGet]
        [ODataRoute("Preferences")]
        public async Task<IActionResult> GetPreferences()
        {
            try
            {
                /*
                 * Se obtienen las preferencias de usuario
                 * Es imprescindible que devuelva un TASK por si tiene que llamar a alguna api
                 */

                var clientId = Guid.NewGuid();//Obtenemos el ID del JWT (Json Web Tokken)

                var userPReferences = await Preferences.Calculate(clientId); //Obtención del las preferencias
                //Filtramos
                var result = await Service.Get(x => x.Price > userPReferences.MinPrice && x.Price < userPReferences.MaxPrice);

                return Ok(result);
            }
            catch (Exception oEx)
            {
                return SendError("GET", oEx);
            }
        }

        public async Task<IActionResult> GetOfferType([FromODataUri] Guid key)
        {
            try
            {
                var result = await Service.Find(key);
                return Ok(await OfferTypeService.Find(result.OfferTypeId));
            }
            catch (Exception oEx)
            {
                return SendError("GetTranslations", oEx);
            }
        }
    }
}

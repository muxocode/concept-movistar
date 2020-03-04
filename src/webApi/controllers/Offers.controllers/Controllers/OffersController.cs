using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crossapp.file.logging;
using crossapp.repository;
using crossapp.services;
using crossapp.unitOfWork;
using entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Offers.webApi.Controllers
{

    public class OffersController : ControllerBase<Offer, Guid>
    {
        private readonly IService<OfferType> OfferTypeService;

        public OffersController(IService<Offer> service, IService<OfferType> offerTypeService, IUnitOfWork unitOfWork, ILogHandler errorHandler) 
            :base(service, unitOfWork, errorHandler)
        {
            OfferTypeService = offerTypeService;
        }

        public override async Task<IActionResult> Get()
        {
            try
            {
                /*
                 * En este punto habría que calcular las ofertas del cliente
                 * 
                 */
                var result = await Service.Get();
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

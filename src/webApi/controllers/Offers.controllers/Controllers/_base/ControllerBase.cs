using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using canalonline.data._base;
using crossapp.file.logging;
using crossapp.services;
using crossapp.unitOfWork;
using entities._base;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace Offers.webApi.Controllers
{

    [Route("[controller]")]
    [EnableQuery()]
    public abstract class ControllerBase<T, TKey> : ODataController where T : EntityBase<TKey>
    {
        protected readonly IService<T> Service;
        protected readonly ILogHandler ErrorHandler;
        protected readonly IUnitOfWork UnitOfWork;

        protected ControllerBase(IService<T> service, IUnitOfWork unitOfWork, ILogHandler errorHandler)
        {
            Service = service;
            ErrorHandler = errorHandler;
            UnitOfWork = unitOfWork;
        }

        protected IActionResult SendError(string name, Exception ex)
        {
            var text = $"Error on {name} in {this.Request.Path}";
            this.ErrorHandler.Trace(text, ex);
            return BadRequest(text);
        }

        // GET
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                var result = await Service.Get();
                return Ok(result);
            }
            catch (Exception oEx)
            {
                return SendError("GET", oEx);
            }
        }

        // GET/key
        public virtual async Task<IActionResult> Get([FromODataUri]TKey key)
        {
            try
            {
                //Esto es una excepción de EntityFramwork, incumple a LisKov, pero soy consciente y está pendiente un parche por parte de Microsoft
                if (this.Service is ICollector<T> collector && collector.Set != null)
                {
                    return Ok((SingleResult.Create(collector.Set.Where(x => x.Id.Equals(key)))));
                }
                else
                {
                    return Ok(SingleResult.Create((await this.Service.Get()).Where(x => x.Id.Equals(key)).AsQueryable()));
                }
            }
            catch (Exception oEx)
            {
                return SendError($"GET({key})", oEx);
            }
        }


        // POST: api/Enneatype
        //[HttpPost]
        public virtual async Task<IActionResult> Post(T item)
        {
            try
            {
                IActionResult result;
                if (!ModelState.IsValid)
                {
                    result = BadRequest(ModelState);
                }
                else
                {
                    await Service.Insert(item);
                    await this.UnitOfWork.SaveChanges();

                    return Created(item);
                }

                return result;
            }
            catch (Exception oEx)
            {
                return SendError($"POST [{JsonSerializer.Serialize(item)}]", oEx);
            }
        }

        // PUT: api/Enneatype/5
        //[HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromODataUri] TKey key, T item)
        {
            try
            {
                IActionResult result;
                if (!ModelState.IsValid)
                {
                    result = BadRequest(ModelState);
                }
                else
                {
                    if (!key.Equals(item.Id))
                    {
                        result = BadRequest();
                    }
                    else
                    {
                        await Service.Update(item);
                        await UnitOfWork.SaveChanges();
                        result = Updated(item);
                    }
                }

                return result;
            }
            catch (Exception oEx)
            {
                return SendError($"PUT({key})[{JsonSerializer.Serialize(item)}]", oEx);
            }
        }

        public virtual async Task<IActionResult> Patch([FromODataUri] TKey key, Delta<T> item)
        {
            try
            {
                IActionResult result;

                if (!ModelState.IsValid)
                {
                    result = BadRequest(ModelState);
                }
                else
                {
                    var entity = await Service.Find(key);
                    if (entity == null)
                    {
                        result = NotFound();
                    }
                    else
                    {
                        item.Patch(entity);

                        await Service.Update(entity);
                        await UnitOfWork.SaveChanges();
                        result = Updated(item);
                    }
                }

                return result;
            }
            catch (Exception oEx)
            {
                return SendError($"Patch({key})[{JsonSerializer.Serialize(item)}]", oEx);
            }
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete([FromODataUri] TKey key)
        {
            try
            {
                await Service.Remove(await Service.Find(key));
                await UnitOfWork.SaveChanges();
                return StatusCode((int)System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception oEx)
            {
                return SendError($"Delete({key})", oEx);
            }
        }
    }


}

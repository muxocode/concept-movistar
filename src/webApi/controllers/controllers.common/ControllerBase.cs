using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using canalonline.data._base;
using crossapp.log.logging;
using crossapp.services;
using crossapp.unitOfWork;
using entities._base;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace webApi.common.Controllers
{
    /// <summary>
    /// Controllers base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    [Route("[controller]")]
    [EnableQuery()]
    public abstract class ControllerBase<T, TKey> : ODataController where T : EntityBase<TKey>
    {
        protected readonly IService<T> Service;
        protected readonly ILogHandler ErrorHandler;
        protected readonly IUnitOfWork UnitOfWork;

        //Es importante que el desarrollador sea consciente de la dotación del api
        //Por defecto el api no acepta llamadas
        protected virtual bool Read { get; } = false;
        protected virtual bool Writte { get; } = false;


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
                if (!this.Read)
                    throw new InvalidOperationException("Not permited");

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
                if (!this.Read)
                    throw new InvalidOperationException("Not permited");

                //Esto es una excepción de EntityFramwork, incumple a LisKov, soy consciente y está pendiente un parche por parte de Microsoft.
                //Parece que pierde el IQuerable durante la ejecución del hilo
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


        // POST
        public virtual async Task<IActionResult> Post(T item)
        {
            try
            {
                if (!this.Writte)
                    throw new InvalidOperationException("Not permited");

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

        // PUT
        public virtual async Task<IActionResult> Put([FromODataUri] TKey key, T item)
        {
            try
            {
                if (!this.Writte)
                    throw new InvalidOperationException("Not permited");

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

        //PATCH
        public virtual async Task<IActionResult> Patch([FromODataUri] TKey key, Delta<T> item)
        {
            try
            {
                if (!this.Writte)
                    throw new InvalidOperationException("Not permited");

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

        // DELETE
        public virtual async Task<IActionResult> Delete([FromODataUri] TKey key)
        {
            try
            {
                if (!this.Writte)
                    throw new InvalidOperationException("Not permited");

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

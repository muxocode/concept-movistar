using crossapp.unitOfWork;
using entities._base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace canalonline.data._base
{
    /// <summary>
    /// Repository pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : ICollector<T>, crossapp.repository.IEntityRepository<T> where T : class, IEntity
    {
        private readonly DbContext context;
        private readonly IEntityUnitOfWork UnitOfWork;

        protected RepositoryBase(IEntityUnitOfWork unitOfWork, movistarContext context)
        {
            UnitOfWork = unitOfWork;
            this.context = context;
        }

        public DbSet<T> Set => context.Set<T>();

        /// <summary>
        /// Insert an element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task Add(T item)
        {
            //Se añadiría el elemento a BBDD
            await this.UnitOfWork.Add(item);
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Gets an element
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> Find<TKey>(TKey key)
        {
            //Se busca en BBDD
            return context.Set<T>().Find(key);
        }

        /// <summary>
        /// Get a collection
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression = null)
        {
            //Se busca en BBDD
            IEnumerable<T> result = context.Set<T>();

            //Si hay filtro, se filtra
            if (expression != null)
            {
                result = context.Set<T>().Where(expression?.Compile()).ToList();
            }

            return result;
        }

        /// <summary>
        /// Remove a collection of elements
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public virtual async Task Remove(params T[] items)
        {
            //Prepara para guardar una serie de items

            var aTasks = new List<Task>();

            foreach (var item in items)
            {
                aTasks.Add(this.Remove(item));
            }

            Task.WaitAll(aTasks.ToArray());
        }

        /// <summary>
        /// Removes a element
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task Remove(T obj)
        {
            await UnitOfWork.Remove(obj);
        }

        /// <summary>
        /// Update an element
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task Update(T obj)
        {
            // Prepara el obj para hacer un update
            await UnitOfWork.Update(obj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crossapp.services
{
    /// <summary>
    /// Repository with actions asociated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Get a set of entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null);
        /// <summary>
        /// Get a specific entity
        /// </summary>
        /// <param name="key">Unique key</param>
        /// <returns></returns>
        Task<T> Find<TKey>(TKey key);
        /// <summary>
        /// Update an entity
        /// </summary>
        /// <returns></returns>
        Task Update(T obj);
        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <returns></returns>
        Task<T> Insert(T obj);
        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <returns></returns>
        Task Remove(T obj);
    }
}

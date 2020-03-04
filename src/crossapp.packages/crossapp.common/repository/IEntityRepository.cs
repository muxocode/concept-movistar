using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crossapp.repository
{
    /// <summary>
    /// Repository pattern for entities
    /// </summary>
    public interface IEntityRepository<T>:IRepository<T>
    {
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
        /// Remove an entity
        /// </summary>
        /// <param name="obj">Item</param>
        /// <returns></returns>
        Task Remove(T obj);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crossapp.repository
{
    /// <summary>
    /// Repository pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>:IDisposable
    {
        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="item">item to add</param>
        Task Add(T item);
        /// <summary>
        /// Gets elements from repository
        /// </summary>
        /// <param name="expression">Filter</param>
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression = null);
        /// <summary>
        /// Removes items from repository
        /// </summary>
        Task Remove(params T[] items);
    }
}

using crossapp.unitOfWork.enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace crossapp.unitOfWork
{
    /// <summary>
    /// Pending entities
    /// </summary>
    public interface IEntityUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Entity</param>
        Task Add<T>(T obj) where T : class;
        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Entity</param>
        /// <returns></returns>
        Task Update<T>(T obj) where T : class;
        /// <summary>
        /// Removes an object
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="obj">Entity</param>
        /// <returns></returns>
        Task Remove<T>(T obj) where T : class;
    }
}

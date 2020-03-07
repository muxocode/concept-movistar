using entities._base;
using Microsoft.EntityFrameworkCore;

namespace canalonline.data._base
{
    /// <summary>
    /// Class that access to BBDD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollector<T> where T : class, IEntity
    {
        /// <summary>
        /// Table
        /// </summary>
        DbSet<T> Set { get;}
    }
}
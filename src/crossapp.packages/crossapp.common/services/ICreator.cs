using System.Threading.Tasks;

namespace crossapp.services
{
    /// <summary>
    /// Creates an entity of <typeparamref name="TOut"/> from <typeparamref name="TIn"/> value
    /// </summary>
    /// <typeparam name="TOut">out entity</typeparam>
    /// <typeparam name="TIn">input entity</typeparam>
    public interface ICreator<TOut, in TIn>
    {
        /// <summary>
        /// Create an <typeparamref name="TOut"/> entity
        /// </summary>
        /// <param name="data"><typeparamref name="TIn"/> value</param>
        /// <returns></returns>
        Task<TOut> Create(TIn data);
    }

    /// <summary>
    /// Creates a <typeparamref name="TOut"/> entity
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface ICreator<TOut>
    {
        /// <summary>
        /// Creates a <typeparamref name="TOut"/> entity
        /// </summary>
        /// <returns></returns>
        Task<TOut> Create();
    }
}

using System.Threading.Tasks;

namespace crossapp.rules
{
    /// <summary>
    /// Rule
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface IRule<T>
    {
        /// <summary>
        /// Check the rule
        /// </summary>
        /// <param name="obj">entity</param>
        /// <returns></returns>
        Task<bool> Check(T obj);
    }
}

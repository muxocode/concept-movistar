using crossapp.rules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace domain.rules
{
    /// <summary>
    /// Rule processor
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class RuleProcessor<T> : IRuleProcessor<T> where T : class
    {
        /// <summary>
        /// Object that checks the rules of T entity
        /// </summary>
        public IEnumerable<IRule<T>> Rules { get; set; }
        /// <summary>
        /// Create a RuleProcessor
        /// </summary>
        /// <param name="rules">Rules to check</param>
        public RuleProcessor(IEnumerable<IRule<T>> rules)
        {
            this.Rules = rules;
        }

        /// <summary>
        /// Check rules
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        public async Task<bool> CheckRules(T entity)
        {
            var bResult = true;
            if (this.Rules != null)
            {
                var result = await Task.WhenAll(Rules.Select(x => x.Check(entity)));
                foreach (var item in result)
                {
                    bResult &= item;
                }
            }
            return bResult;
        }
    }
}

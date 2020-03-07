using canalonline.data._base;
using crossapp.repository;
using crossapp.rules;
using crossapp.services;
using entities._base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace domain.services._base
{
    public class ServiceGeneric<T> : ICollector<T>, IService<T> where T : class, IEntity
    {
        readonly IEntityRepository<T> Repository;
        readonly IRuleProcessor<T> RuleProcesor;
        public ServiceGeneric(IEntityRepository<T> repository, IRuleProcessor<T> ruleProcesor)
        {
            Repository = repository;
            RuleProcesor = ruleProcesor;
        }

        public DbSet<T> Set => (this.Repository as ICollector<T>)?.Set;

        public virtual Task<T> Find<TKey>(TKey key)
        {
            return this.Repository.Find(key);
        }

        public virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null)
        {
            return this.Repository.Get(expression: filter);
        }

        public virtual async Task<T> Insert(T obj)
        {
            await RuleProcesor.CheckRules(obj);
            await Repository.Add(obj);
            return obj;
        }

        public virtual Task Remove(T obj)
        {
            return Repository.Remove(obj);
        }

        public virtual async Task Update(T obj)
        {
            await RuleProcesor.CheckRules(obj);
            await Repository.Update(obj);
        }
    }
}

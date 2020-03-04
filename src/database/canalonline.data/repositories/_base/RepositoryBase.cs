using crossapp.unitOfWork;
using entities._base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace canalonline.data._base
{
    public class RepositoryBase<T> : ICollector<T>, crossapp.repository.IEntityRepository<T> where T : class, IEntity
    {
        private readonly DbContext context;
        private readonly IEntityUnitOfWork UnitOfWork;

        public RepositoryBase(IEntityUnitOfWork unitOfWork, movistarContext context)
        {
            UnitOfWork = unitOfWork;
            this.context = context;
        }

        public DbSet<T> Set => context.Set<T>();

        public async Task Add(T item)
        {
            //Se añadiría el elemento a BBDD
            await this.UnitOfWork.Add(item);
        }

        public void Dispose()
        {

        }

        public async Task<T> Find<TKey>(TKey key)
        {
            //Se busca en BBDD
            return context.Set<T>().Find(key);
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression = null)
        {
            //Se busca en BBDD
            IEnumerable<T> result = context.Set<T>();

            if (expression != null)
            {
                result = context.Set<T>().Where(expression?.Compile()).ToList();
            }

            return result;
        }

        public async Task Remove(params T[] items)
        {
            //Prepara para guardar una serie de items

            var aTasks = new List<Task>();

            foreach (var item in items)
            {
                aTasks.Add(this.Remove(item));
            }

            Task.WaitAll(aTasks.ToArray());
        }

        public async Task Remove(T obj)
        {
            await UnitOfWork.Remove(obj);
        }

        public async Task Update(T obj)
        {
            // Prepara el obj para hacer un update
            await UnitOfWork.Update(obj);
        }
    }
}

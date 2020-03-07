using crossapp.unitOfWork;
using System.Threading.Tasks;

namespace canalonline.data
{
    /// <summary>
    /// Unit of work pattern
    /// </summary>
    public class UnitOfWork : IEntityUnitOfWork
    {
        public UnitOfWork(movistarContext context)
        {
            Context = context;
        }

        public movistarContext Context { get; }

        public async Task Add<T>(T obj) where T : class
        {
            Context.Add(obj);
        }

        public async Task DiscardChanges()
        {
            
        }

        public async void Dispose()
        {
            
        }

        public async Task Remove<T>(T obj) where T : class
        {
            Context.Remove(obj);
        }


        public async Task SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task Update<T>(T obj) where T : class
        {
            Context.Remove(obj);
        }

    }
}

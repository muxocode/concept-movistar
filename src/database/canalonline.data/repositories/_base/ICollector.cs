using entities._base;
using Microsoft.EntityFrameworkCore;

namespace canalonline.data._base
{
    public interface ICollector<T> where T : class, IEntity
    {
        DbSet<T> Set { get;}
    }
}
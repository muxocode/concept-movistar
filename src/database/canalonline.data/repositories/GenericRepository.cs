using crossapp.unitOfWork;
using entities._base;

namespace canalonline.data
{
    public class GenericRepository<T> : _base.RepositoryBase<T> where T : class, IEntity
    {
        /// <summary>
        /// Generic repository for general porpouse
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="context"></param>
        public GenericRepository(IEntityUnitOfWork unitOfWork, movistarContext context) :base(unitOfWork, context)
        {

        }


    }
}

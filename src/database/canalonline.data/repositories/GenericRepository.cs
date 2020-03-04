using crossapp.unitOfWork;
using entities;
using entities._base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace canalonline.data
{
    public class GenericRepository<T> : _base.RepositoryBase<T> where T : class, IEntity
    {
        public GenericRepository(IEntityUnitOfWork unitOfWork, movistarContext context) :base(unitOfWork, context)
        {

        }


    }
}

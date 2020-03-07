using crossapp.repository;
using crossapp.rules;
using entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace domain.services
{
    public class ExampleService: _base.ServiceGeneric<Offer>
    {
        public ExampleService(IEntityRepository<Offer> repository, IRuleProcessor<Offer> ruleProcesor) :base(repository, ruleProcesor)
        {
            
        }

        public override Task<IEnumerable<Offer>> Get(Expression<Func<Offer, bool>> filter = null)
        {


            return base.Get(filter);    
        }
    }
}

using crossapp.rules;
using entities;
using System;
using System.Threading.Tasks;

namespace domain.rules.offer
{
    public class Checkstate : IRule<OffersClients>
    {
        public Task<bool> Check(OffersClients obj)
        {
            //Comprueba si uno de lso tres estados está chequeado
            var result = obj.Visited.GetValueOrDefault() || obj.Showed.GetValueOrDefault() || obj.Searched.GetValueOrDefault();
            if (!result)
            {
                throw new ArgumentException("One state must be checked");
            }

            return Task.Run(()=>true);
        }
    }
}

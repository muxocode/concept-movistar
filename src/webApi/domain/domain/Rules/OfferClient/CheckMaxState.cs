using crossapp.rules;
using entities;
using System;
using System.Threading.Tasks;

namespace domain.rules.offer
{
    public class CheckMaxState : IRule<OffersClients>
    {
        public Task<bool> Check(OffersClients obj)
        {
            //Comprueba si está checkeado como mucho un estado
            var result = 0;

            if (obj.Visited.GetValueOrDefault()) {
                result++;
            }

            if (obj.Showed.GetValueOrDefault())
            {
                result++;
            }

            if (obj.Searched.GetValueOrDefault())
            {
                result++;
            }

           
            if (result > 1)
            {
                throw new ArgumentException("Only one state must be checked");
            }

            return Task.Run(()=>true);
        }
    }
}

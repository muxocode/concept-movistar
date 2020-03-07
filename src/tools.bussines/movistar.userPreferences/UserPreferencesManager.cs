using movistar.model.bussines;
using System;
using System.Threading.Tasks;

namespace movistar.userPreferences
{
    internal class UserPreferencesManager : IUserPreferencesManager
    {
        public Task<IPreferences> Calculate(Guid clientId)
        {
            /*
             * Esta clase está diseñada para le demo.
             * La intención sería que accediera a una API, lanzara de procesos de IA, etc
             */

            return Task.Run<IPreferences>(() => new classes.UserPreferences
            {
                MaxPrice = 250,
                MinPrice = 200
            });
        }
    }
}

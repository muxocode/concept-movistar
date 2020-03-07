using System;
using System.Threading.Tasks;

namespace movistar.model.bussines
{
    public interface IUserPreferencesManager
    {
        Task<IPreferences> Calculate(Guid clientId);
    }
}
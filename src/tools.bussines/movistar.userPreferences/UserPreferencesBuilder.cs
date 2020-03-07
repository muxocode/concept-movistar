using movistar.model.bussines;

namespace movistar.userPreferences
{
    /// <summary>
    /// Builder Pattern
    /// </summary>
    public static class UserPreferencesBuilder
    {
        public static IUserPreferencesManager Create()
        {
            return new UserPreferencesManager();
        }
    }
}

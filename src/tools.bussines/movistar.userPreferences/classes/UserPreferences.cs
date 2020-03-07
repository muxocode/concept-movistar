using movistar.model.bussines;

namespace movistar.userPreferences.classes
{
    internal class UserPreferences : IPreferences
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}

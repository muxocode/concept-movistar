namespace movistar.model.bussines
{
    /// <summary>
    /// User preferences
    /// </summary>
    public interface IPreferences
    {
        /// <summary>
        /// Min price
        /// </summary>
        decimal MinPrice { get; set; }
        /// <summary>
        /// MaxPrice
        /// </summary>
        decimal MaxPrice { get; set; }
    }
}

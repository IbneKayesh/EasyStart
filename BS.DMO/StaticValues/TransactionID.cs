namespace BS.DMO.StaticValues
{
    public static class TransactionID
    {
        /// <summary>
        /// Sales Booking
        /// </summary>
        public static string SB = "SB";

        public static List<string> GetAll()
        {
            return new List<string>()
            {
                "SB",
            };
        }
    }
}
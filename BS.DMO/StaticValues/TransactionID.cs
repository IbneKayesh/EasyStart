namespace BS.DMO.StaticValues
{
    public static class TransactionID
    {
        /// <summary>
        /// Sales Booking
        /// </summary>
        public static string SB = "SB";

        /// <summary>
        /// Bank Loan Capital
        /// </summary>
        public static string BLC = "BLC";

        public static List<string> GetAll()
        {
            return new List<string>()
            {
                "SB",
                "BLC",
            };
        }
    }
}
namespace BS.DMO.StaticValues
{
    public static class EntityValueText
    {
        /// <summary>
        /// SB :: Sales Order Booking Source
        /// </summary>
        public static string BOOKING_SOURCE = "BOOKING_SOURCE";

        /// <summary>
        /// Payment Mode, Cash / Credit
        /// </summary>
        public static string PAYMENT_MODE = "PAYMENT_MODE";

        /// <summary>
        /// Payment Method, By LC, BBLC, Cash
        /// </summary>
        public static string PAYMENT_METHOD = "PAYMENT_METHOD";

        /// <summary>
        /// SB :: Sales Order Transaction Type ID
        /// </summary>
        public static string SB_TRN_TYPE_ID = "SB_TRN_TYPE_ID";

        /// <summary>
        /// Shipping Type :: By Road, Air, Sea
        /// </summary>
        public static string SHIPPING_TYPE_ID = "SHIPPING_TYPE_ID";

        /// <summary>
        /// Shipping Mode :: Customer Transport, Own Transport
        /// </summary>
        public static string SHIPPING_MODE_ID = "SHIPPING_MODE_ID";

        /// <summary>
        /// Contact Group :: Bulk, Retail
        /// </summary>
        public static string CONTACT_CATEGORY_ID = "CONTACT_CATEGORY_ID";

        public static List<string> GetAll()
        {
            return new List<string>()
            {
                "BOOKING_SOURCE",
                "PAYMENT_MODE",
                "PAYMENT_METHOD",
                "SB_TRN_TYPE_ID",
                "SHIPPING_TYPE_ID",
                "SHIPPING_MODE_ID",
                "CONTACT_CATEGORY_ID",
            };
        }
    }
}

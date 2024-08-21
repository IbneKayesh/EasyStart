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

        
        /// <summary>
        /// Color codes :: Theam, Colors, Hex
        /// </summary>
        public static string COLOR_CODE = "COLOR_CODE";


        /// <summary>
        /// Help Desk : WT_TYPE
        /// </summary>
        public static string WT_TYPE = "WT_TYPE";

        /// <summary>
        /// Help Desk : STATUS_ID
        /// </summary>
        public static string STATUS_ID = "STATUS_ID";

        /// <summary>
        /// Help Desk : PRIORITY_ID
        /// </summary>
        public static string PRIORITY_ID = "PRIORITY_ID";

        /// <summary>
        /// Transport : PRIORITY_ID
        /// </summary>
        public static string DELIVERY_AGENT_TYPE_ID = "DELIVERY_AGENT_TYPE_ID";
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
                "COLOR_CODE",
                "WT_TYPE",
                "STATUS_ID",
                "PRIORITY_ID",
                "DELIVERY_AGENT_TYPE_ID",
            };
        }
    }
}

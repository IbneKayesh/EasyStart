namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CLASS
    {
        public PRODUCT_CLASS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? CLASS_NAME { get; set; }
        public string? CLASS_DESC { get; set; }

        //Discount
        public bool IS_DISCOUNT { get; set; }
        public decimal INVOICE_VALUE { get; set; }
        public decimal DISCOUNT_PCT { get; set; }
        public decimal DISCOUNT_VALUE { get; set; }
    }
}

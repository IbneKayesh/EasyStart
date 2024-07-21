namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_STATUS
    {
        public PRODUCT_STATUS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? PRODUCT_CATEGORY_NAME { get; set; }
        public string? PRODUCT_CATEGORY_DESC { get; set; }
    }
}

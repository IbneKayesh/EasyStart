namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_STATUS
    {
        public PRODUCT_STATUS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? STATUS_NAME { get; set; }
        public string? STATUS_DESC { get; set; }
    }
}

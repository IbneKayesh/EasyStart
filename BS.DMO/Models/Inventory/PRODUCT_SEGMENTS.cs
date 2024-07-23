namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_SEGMENTS
    {
        public PRODUCT_SEGMENTS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string SEGMENT_NAME_TYPE_ID { get; set; }
        public string SEGMENT_NAME_TYPE_VALUE { get; set; }
    }
}

namespace BS.DMO.Models.Company
{
    public class BUSINESS_COST_CENTER
    {
        public int ID { get; set; }
        public int BUSINESS_ID { get; set; }


        public string? COST_CENTER_NAME { get; set; }
        public decimal MAX_LIMIT { get; set; }
        public DateTime OPEN_DATE { get; set; }
    }
}

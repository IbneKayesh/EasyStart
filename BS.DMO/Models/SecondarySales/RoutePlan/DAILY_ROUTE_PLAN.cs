namespace BS.DMO.Models.SecondarySales.RoutePlan
{
    public class DAILY_ROUTE_PLAN
    {
        public DAILY_ROUTE_PLAN()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? DAY_NAME { get; set; }
        public string? ROUTE_NO { get; set; }
        public string? ROUTE_DESC { get; set; }

        public string? START_DISTRIBUTOR_ID { get; set; }
        public string? END_DISTRIBUTOR_ID { get; set; }

        public string? NO_OF_STOP_POINT { get; set; }
        public decimal? TOTAL_DISTANCE { get; set; }
    }
}
namespace BS.DMO.Models.SecondarySales.RoutePlan
{
    public class DAILY_ROUTE_PLAN_CHILD
    {
        public DAILY_ROUTE_PLAN_CHILD()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? DAILY_ROUTE_PLAN_ID { get; set; }

        public string? DISTRIBUTOR_ID { get; set; }

        public int POINT_NO { get; set; }

        public DateTime PREF_START_TIME { get; set; }
        public DateTime PREF_END_TIME { get; set; }
        public int TOTAL_MINS { get; set; }

        public decimal PREV_DISTANCE { get; set; }
        public decimal NEXT_DISTANCE { get; set; }
        public decimal BETWEEN_DISTANCE { get; set; }


        public string? PREV_DISTRIBUTOR_ID { get; set; }
        public string? NEXT_DISTRIBUTOR_ID { get; set; }
    }
}

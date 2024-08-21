namespace BS.DMO.Models.SecondarySales.RoutePlanning
{
    public class DAILY_ROUTE_PLAN_CHILD_VISIT
    {
        public DAILY_ROUTE_PLAN_CHILD_VISIT()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? DISTRIBUTOR_ID { get; set; }
        public string? SR_ID { get; set; }

        public DateTime VISIT_DATE_TIME { get; set; }
        public bool IS_VISITED { get; set; }
        public string? VISIT_NOTE { get; set; }
    }
}

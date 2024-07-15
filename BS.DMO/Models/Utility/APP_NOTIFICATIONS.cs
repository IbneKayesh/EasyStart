namespace BS.DMO.Models.Utility
{
    public class APP_NOTIFICATIONS
    {
        public APP_NOTIFICATIONS()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? TITLE_TEXT { get; set; }
        public string? BODY_TEXT { get; set; }
        public string? NAV_URL { get; set; }
        public string? TO_USER { get; set; }
        public string? TO_USER_GROUP { get; set; }
        public string? FROM_USER { get; set; }
        public string? PRIORITY_LEVEL { get; set; }
        public bool? IS_READ { get; set; }
        public DateTime? READ_TIME { get; set; }
    }
}

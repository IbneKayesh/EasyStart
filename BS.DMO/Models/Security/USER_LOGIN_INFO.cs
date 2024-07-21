namespace BS.DMO.Models.Security
{
    public class USER_LOGIN_INFO
    {
        public USER_LOGIN_INFO()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? USER_ID { get; set; }
        public string? SESSION_ID { get; set; }
        public DateTime IN_TIME { get; set; }
        public DateTime? OUT_TIME { get; set; }
    }
}

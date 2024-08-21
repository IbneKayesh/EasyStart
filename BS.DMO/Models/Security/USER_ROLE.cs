namespace BS.DMO.Models.Setup.Security
{
    public class USER_ROLE
    {
        public USER_ROLE()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? USER_ID { get; set; }
        public string? ROLE_ID { get; set; }
    }
}
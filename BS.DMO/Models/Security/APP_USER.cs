namespace BS.DMO.Models.Setup.Security
{
    public class APP_USER
    {
        public APP_USER()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }

        public string? USER_NAME { get; set; }
        public string? USER_PASS { get; set; }
        public bool PASS_SELF_RECOVERY { get; set; }
        public string? USER_EMAIL { get; set; }
        public bool EMAIL_VERIFIED { get; set; }
        public DateTime? VALID_UNTIL { get; set; }
        public DateTime? LAST_LOGIN { get; set; }
    }
}
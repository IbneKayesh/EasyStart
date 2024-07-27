namespace BS.DMO.Models.Setup.Security
{
   public class SECURITY_ROLE
    {
        public SECURITY_ROLE()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }

        public string? ROLE_NAME { get; set; }
    }
}

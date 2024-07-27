namespace BS.DMO.Models.Setup.Security
{
    public class MENU_ROLE
    {
        public MENU_ROLE()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? MENU_ID { get; set; }
        public string? ROLE_ID { get; set; }
        
        public bool IS_SELECT { get; set; }
        public bool IS_INSERT { get; set; }
        public bool IS_UPDATE { get; set; }
        public bool IS_DELETE { get; set; }
    }
}
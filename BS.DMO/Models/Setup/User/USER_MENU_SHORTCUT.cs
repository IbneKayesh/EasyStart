namespace BS.DMO.Models.Setup.User
{
    public class USER_MENU_SHORTCUT
    {
        public USER_MENU_SHORTCUT()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? USER_ID { get; set; }
        public string? URL_ID { get; set; }
    }
}

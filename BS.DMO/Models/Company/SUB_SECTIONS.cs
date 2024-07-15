namespace BS.DMO.Models.Company
{
    public class SUB_SECTIONS
    {
        public SUB_SECTIONS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? SECTION_ID { get; set; }


        public string? SUB_SECTION_NAME { get; set; }
        public string? SHORT_NAME { get; set; }
        public string? ADDRESS_INFO { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_NO { get; set; }
        public string? EMAIL_ADDRESS { get; set; }
        public int MAX_EMPLOYEE { get; set; }
        public int MAX_SALARY { get; set; }
    }
}

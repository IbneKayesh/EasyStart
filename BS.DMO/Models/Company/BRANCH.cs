namespace BS.DMO.Models.Company
{

    public class BRANCH
    {
        public BRANCH()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? BRANCH_TYPE_ID { get; set; }
        public string? BUSINESS_ID { get; set; }


        public string? BRANCH_NAME { get; set; }
        public string? SHORT_NAME { get; set; }
        public string? ADDRESS_INFO { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_NO { get; set; }
        public string? EMAIL_ADDRESS { get; set; }
        public int MAX_EMPLOYEE { get; set; }
        public int MAX_SALARY { get; set; }
    }
}

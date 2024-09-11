namespace BS.DMO.StaticValues
{
    public class CommonData
    {
        public static List<string> GetGender()
        {
            return new List<string>()
            {
                "Male",
                "Female",
                "Unknown",
            };
        }
        public static List<string> GetMaritalStatus()
        {
            return new List<string>()
            {
                "Unmarried",
                "Married",
                "Divorced",
                "Unknown",
            };
        }
        public static List<string> GetNationality()
        {
            return new List<string>()
            {
                "Bangladeshi",
                "Foreign",
                "Unknown",
            };
        }
        public static List<string> GetBloodGroup()
        {
            return new List<string>()
            {
                "A positive (A+)",
                "A negative (A-)",
                "B positive (B+)",
                "B negative (B-)",
                "AB positive (AB+)",
                "AB negative (AB-)",
                "O positive (O+)",
                "O negative (O-)",
                "Unknown",
            };
        }
    }
}

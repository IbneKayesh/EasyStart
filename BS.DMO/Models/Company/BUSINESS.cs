using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Company
{
    public class BUSINESS
    {
        public int ID { get; set; }

        public string? BUSINESS_NAME { get; set; }
        public string? SHORT_NAME { get; set; }
        public string? ADDRESS_INFO { get; set; }
        public string? CONTACT_NAME { get; set; }
        public string? CONTACT_NO { get; set; }
        public string? EMAIL_ADDRESS { get; set; }
        public int MAX_EMPLOYEE { get; set; }
        public int MAX_SALARY { get; set; }
    }
}

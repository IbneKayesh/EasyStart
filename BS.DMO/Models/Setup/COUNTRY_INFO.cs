using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class COUNTRY_INFO
    {
        public COUNTRY_INFO()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? COUNTRY_NAME { get; set; }
    }
}

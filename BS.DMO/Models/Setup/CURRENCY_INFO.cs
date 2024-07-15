using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class CURRENCY_INFO
    {
        public CURRENCY_INFO()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? COUNTRY_ID { get; set; }

        public string? CURRENCY_NAME { get; set; }
        public string? CURRENCY_SIGN { get; set; }
        public string? CURRENCY_DESC { get; set; }

        /// <summary>
        /// Base currency should only one in CURRENCY_INFO table
        /// </summary>
        public bool IS_BASE_CURRENCY { get; set; }
    }
}

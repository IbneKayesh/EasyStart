using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class CURRENCY_CONV_RATE
    {
        public CURRENCY_CONV_RATE()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? CURRENCY_ID { get; set; }


        public string? MONTH_ID { get; set; }
        public string? YEAR_ID { get; set; }


        public string? CONVERSION_RATE { get; set; }
    }
}

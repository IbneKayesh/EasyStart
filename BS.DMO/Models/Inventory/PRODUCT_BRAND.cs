using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_BRAND
    {
        public PRODUCT_BRAND()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? COUNTRY_ID { get; set; }


        public string? BRAND_NAME { get; set; }
        public string? BRAND_DESC { get; set; }
    }
}

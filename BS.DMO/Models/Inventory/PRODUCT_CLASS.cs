using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CLASS
    {
        public PRODUCT_CLASS()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? PRODUCT_CLASS_NAME { get; set; }
        public string? PRODUCT_CLASS_DESC { get; set; }
        public bool IS_DISCOUNT { get; set; }
        public decimal DISCOUNT_PCT { get; set; }
        public decimal DISCOUNT_VALUE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CHILD
    {
        public PRODUCT_CHILD()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? UNIT_CHILD_ID { get; set; }
        public string? PRODUCT_BRAND_ID { get; set; }
        public string? PRODUCT_TYPE_ID { get; set; }
        public string? PRODUCT_CLASS_ID { get; set; }
        public string? PRODUCT_CATEGORY_ID { get; set; }


        public string? BAR_CODE { get; set; }
        public string? PRODUCT_NAME { get; set; }
        public string? PRODUCT_DESC { get; set; }
        public string? PRODUCT_IMG { get; set; }
        public decimal LAST_PURCHASE_RATE { get; set; }
        public decimal LAST_SALES_RATE { get; set; }
    }
}

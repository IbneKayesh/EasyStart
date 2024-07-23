using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_TYPE
    {
        public PRODUCT_TYPE()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? TYPE_NAME { get; set; }
        public string? TYPE_DESC { get; set; }

        public bool IS_MASTER_PRODUCT { get; set; }
        public bool IS_PURCHASE { get; set; }
        public bool IS_SALES { get; set; }
        public bool IS_STOCK { get; set; }
    }
}

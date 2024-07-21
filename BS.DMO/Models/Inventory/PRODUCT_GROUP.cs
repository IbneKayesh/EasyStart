using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_GROUP
    {
        public PRODUCT_GROUP()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? PRODUCT_CATEGORY_NAME { get; set; }
        public string? PRODUCT_CATEGORY_DESC { get; set; }
    }
}

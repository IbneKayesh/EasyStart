using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CATEGORY
    {
        public PRODUCT_CATEGORY()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? CATEGORY_NAME { get; set; }
        public string? CATEGORY_DESC { get; set; }
    }
}

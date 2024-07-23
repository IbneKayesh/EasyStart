using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_SOURCE
    {
        public PRODUCT_SOURCE()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? SOURCE_NAME { get; set; }
        public string? SOURCE_DESC { get; set; }
    }
}

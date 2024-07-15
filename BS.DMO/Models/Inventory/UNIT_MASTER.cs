using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class UNIT_MASTER
    {
        public UNIT_MASTER()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? UNIT_MASTER_NAME { get; set; }
        public string? UNIT_MASTER_SHORT_NAME { get; set; }
    }
}

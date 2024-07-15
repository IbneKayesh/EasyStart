using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class UNIT_CHILD
    {
        public UNIT_CHILD()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? UNIT_MASTER_ID { get; set; }



        public string? UNIT_NAME { get; set; }
        public string? SHORT_NAME { get; set; }
        public int RELATIVE_FACTOR { get; set; }
    }
}

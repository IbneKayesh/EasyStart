using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class SEGMENT_NAME_TYPE
    {
        public SEGMENT_NAME_TYPE()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }


        public string? SEGMENT_NAME { get; set; }
        public string? SEGMENT_TYPE { get; set; }
    }
}

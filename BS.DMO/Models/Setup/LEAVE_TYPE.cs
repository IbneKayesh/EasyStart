using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class LEAVE_TYPE
    {
        public LEAVE_TYPE()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }

        public string? LEAVE_TYPE_NAME { get; set; }
        public bool IS_WORKING_DAY { get; set; } = false;
    }
}

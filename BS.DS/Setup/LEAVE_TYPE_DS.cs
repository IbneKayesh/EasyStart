using BS.DMO.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DS.Setup
{
    public class LEAVE_TYPE_DS
    {
        public void Insert()
        {
            List<LEAVE_TYPE> list = new List<LEAVE_TYPE>();
            list.Add(new LEAVE_TYPE { ID = Guid.NewGuid().ToString(), LEAVE_TYPE_NAME = "Weekend", IS_WORKING_DAY = false });
        }
    }
}

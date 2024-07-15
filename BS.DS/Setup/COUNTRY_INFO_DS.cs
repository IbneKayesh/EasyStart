using BS.DMO.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DS.Setup
{
    public class COUNTRY_INFO_DS
    {
        public void Insert()
        {
            List<COUNTRY_INFO> list = new List<COUNTRY_INFO>();
            list.Add(new COUNTRY_INFO { ID = Guid.NewGuid().ToString(), COUNTRY_NAME = "Bangladesh" });
        }
    }
}

using BS.DMO.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DS.Company
{
    public class BRANCH_TYPE_DS
    {
        public void Insert()
        {
            List<BRANCH_TYPE> list = new List<BRANCH_TYPE>();
            list.Add(new BRANCH_TYPE { ID = Guid.NewGuid().ToString(), BRANCH_TYPE_NAME = "Corporate", SHORT_NAME = "CO" });
            list.Add(new BRANCH_TYPE { ID = Guid.NewGuid().ToString(), BRANCH_TYPE_NAME = "Production", SHORT_NAME = "PP" });
            list.Add(new BRANCH_TYPE { ID = Guid.NewGuid().ToString(), BRANCH_TYPE_NAME = "Warehouse", SHORT_NAME = "WH" });
            list.Add(new BRANCH_TYPE { ID = Guid.NewGuid().ToString(), BRANCH_TYPE_NAME = "Shop", SHORT_NAME = "SH" });
            list.Add(new BRANCH_TYPE { ID = Guid.NewGuid().ToString(), BRANCH_TYPE_NAME = "Sub Branch", SHORT_NAME = "SB" });
        }
    }
}

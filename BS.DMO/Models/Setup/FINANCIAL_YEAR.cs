using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class FINANCIAL_YEAR
    {
        public FINANCIAL_YEAR()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public int PERIOD_NO { get; set; }
        public int YEAR_ID { get; set; }
        public string? YEAR_NAME { get; set; }
        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }
        public bool IS_LOCKED { get; set; }
    }
}

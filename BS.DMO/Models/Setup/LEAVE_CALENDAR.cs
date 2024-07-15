using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class LEAVE_CALENDAR
    {
        public LEAVE_CALENDAR()
        {
            ID = Guid.Empty.ToString();
        }
        public string ID { get; set; }
        public string? FINANCIAL_YEAR_ID { get; set; }
        public string? LEAVE_TYPE_ID { get; set; }


        /// <summary>
        /// From FINANCIAL_YEAR > YEAR_ID
        /// </summary>
        public int YEAR_ID { get; set; }

        public DateTime CALENDAR_DATE { get; set; }
        public bool IS_WORKING_DAY { get; set; }
        public string? NOTE_INFO { get; set; }
    }
}

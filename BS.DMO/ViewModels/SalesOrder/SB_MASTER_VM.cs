using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.ViewModels.SalesOrder
{
    public class SB_MASTER_VM : SB_MASTER
    {
        //[NotMapped]
        [Display(Name = "Customer Name")]
        public string? CONTACT_NAME { get; set; }

        [Display(Name = "Office Address")]
        public string? OFFICE_ADDRESS { get; set; }
    }
}

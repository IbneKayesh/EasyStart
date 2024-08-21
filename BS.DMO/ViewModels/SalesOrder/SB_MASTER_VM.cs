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

        [Display(Name = "Customer Bill To")]
        public string? BILL_OFFICE_ADDRESS { get; set; }

        [Display(Name = "Delivery Address")]
        public string? DELIVERY_ADDRESS { get; set; }

        [Display(Name = "To User Name")]
        public string? TO_USER_NAME { get; set; }
    }
}

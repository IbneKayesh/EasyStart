using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    internal class ITEM_SECTION
    {

        [Display(Name = "Min Stock Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal MIN_STOCK_QTY { get; set; }

        [Display(Name = "Max Stock Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal MAX_STOCK_QTY { get; set; }

        [Display(Name = "Purchase Target Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PURCHASE_TARGET_QTY { get; set; }

        [Display(Name = "Sales Target Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal SALES_TARGET_QTY { get; set; }


        [Display(Name = "Stop Purchase")]
        public bool STOP_PURCHASE { get; set; }

        [Display(Name = "Stop Sales")]
        public bool STOP_SALES { get; set; }

        [Display(Name = "Stop Discount")]
        public bool STOP_DISCOUNT { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    internal class ITEM_DETAILS
    {

        [Display(Name = "Weight Per Unit")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? WEIGHT_PER_UNIT { get; set; }
    }
}

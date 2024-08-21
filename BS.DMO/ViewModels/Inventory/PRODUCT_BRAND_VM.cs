using BS.DMO.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.ViewModels.Inventory
{
    public class PRODUCT_BRAND_VM : PRODUCT_BRAND
    {
        //[NotMapped]
        [Display(Name = "Country Name")]
        public string? COUNTRY_NAME { get; set; }
    }
}

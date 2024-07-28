using BS.DMO.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.ViewModels.Application
{
    public class CLASSIC_MENU_VM : CLASSIC_MENU
    {
        [Display(Name = "View")]
        public bool IS_SELECT { get; set; }

        [Display(Name = "Add")]
        public bool IS_INSERT { get; set; }

        [Display(Name = "Edit")]
        public bool IS_UPDATE { get; set; }

        [Display(Name = "Delete")]
        public bool IS_DELETE { get; set; }
    }
}

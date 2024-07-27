using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Security
{
    public class USER_MENU_SHORTCUT
    {
        public USER_MENU_SHORTCUT()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string? USER_ID { get; set; }
        public string? MENU_ID { get; set; }
    }
}

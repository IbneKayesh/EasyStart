
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.ViewModels.Transport
{
    public class DELIVERY_AGENT_VM: DELIVERY_AGENT
    {
        //[NotMapped]

        [Display(Name = "Agent Type")]
        public string? DELIVERY_AGENT_TYPE { get; set; }
    }
}

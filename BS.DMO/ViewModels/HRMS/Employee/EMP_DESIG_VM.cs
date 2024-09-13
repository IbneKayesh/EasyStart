using BS.DMO.Models.HRMS.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.ViewModels.HRMS.Employee
{
    public class EMP_DESIG_VM: EMP_DESIG
    {

        //[Not Mapped]
        [Display(Name = "Designation Name")]
        public string? DESIGNATION_NAME { get; set; }
    }
}

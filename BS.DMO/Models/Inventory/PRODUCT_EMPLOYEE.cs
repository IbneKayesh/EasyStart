using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_EMPLOYEE
    {
        public PRODUCT_EMPLOYEE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string? ID { get; set; }


        [Display(Name = "Product")]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_ID { get; set; }

        [Display(Name = "Employee")]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMPLOYEE_ID { get; set; }

        [Display(Name = "Assign Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime ASSIGN_DATE { get; set; }

        [Display(Name = "Assign Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        public string? ASSIGN_NOTE { get; set; }



        [Display(Name = "Return Date")]
        public DateTime? RETURN_DATE { get; set; }

        [Display(Name = "Is Return")]
        public bool IS_RETURN { get; set; }

        [Display(Name = "Return Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? RETURN_NOTE { get; set; }


        [NotMapped]
        [Display(Name = "Product Name")]
        public string? PRODUCT_NAME { get; set; }

        [NotMapped]
        [Display(Name = "Employee Name")]
        public string? EMPLOYEE_NAME { get; set; }


        [NotMapped]
        [Display(Name = "Product Employees")]
        public List<PRODUCT_CHILD>? PRODUCT_CHILD { get; set; }
    }
}

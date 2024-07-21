using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DMO.Models.Setup
{
    public class ENTITY_NAME_VALUE
    {
        public ENTITY_NAME_VALUE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string? ID { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? VALUE_ID { get; set; }


        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? NAME_ID { get; set; }


        [Display(Name = "Entity Id")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        public string? ENTITY_ID { get; set; }


        [Display(Name = "Entity Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ENTITY_DESCRIPTION { get; set; }
    }
}

namespace BS.DMO.Models.HRMS.Employee
{
    public class EMP_EXPERIENCE : BaseModel
    {
        public EMP_EXPERIENCE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Employee ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMP_ID { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? COMPANY_NAME { get; set; }

        [Display(Name = "Working Area")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(300, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string? WORKING_AREA { get; set; }

        [Display(Name = "Experience Year")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string? EXP_YEAR { get; set; }
    }
}
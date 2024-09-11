namespace BS.DMO.Models.HRMS.Employee
{
    public class EMP_EDU : BaseModel
    {
        public EMP_EDU()
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
        
        [Display(Name = "Is Certificate")]
        public bool IS_CERTIFICATE { get; set; }

        [Display(Name = "Institution/Training Title")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? EDU_TITLE { get; set; }

        [Display(Name = "Passing Year")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string? EDU_YEAR { get; set; }

        [Display(Name = "Passing Grade")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string? EDU_GRADE { get; set; }

        [Display(Name = "Institute Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string? INSTITUE_NAME { get; set; }
    }
}
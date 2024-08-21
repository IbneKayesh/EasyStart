namespace BS.DMO.Models.HRMS.Setup
{
    public class DESIGNATION : BaseModel
    {
        public DESIGNATION()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Senior Level")]
        [Required(ErrorMessage = "{0} is required")]
        public int SENIOR_LEVEL { get; set; }

        [Display(Name = "Short Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHORT_NAME { get; set; }

        [Display(Name = "Designation Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? DESIGNATION_NAME { get; set; }

        [Display(Name = "Parent Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PARENT_ID { get; set; }

        [Display(Name = "Lower Bound Salary")]
        [Required(ErrorMessage = "{0} is required")]
        public int LOWER_BOUND { get; set; }

        [Display(Name = "Upper Bound Salary")]
        [Required(ErrorMessage = "{0} is required")]
        public int UPPER_BOUND { get; set; }
    }
}

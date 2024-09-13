namespace BS.DMO.Models.HRMS.Employee
{
    public class EMP_DESIG : BaseModel
    {        
        public EMP_DESIG()
        {
            ID = Guid.Empty.ToString();
            FROM_DATE = DateTime.Now;
            TO_DATE = DateTime.Now.AddYears(50);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Employee Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? EMP_ID { get; set; }

        [Display(Name = "Designation Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? DESIG_ID { get; set; }

        [Display(Name = "From Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime FROM_DATE { get; set; }

        [Display(Name = "To Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime TO_DATE { get; set; }

        [Display(Name = "Designation Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? DESIG_NOTE { get; set; }
    }
}

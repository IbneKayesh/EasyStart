namespace BS.DMO.Models.HRMS.Employee
{
    public class EMP_ADDRESS : BaseModel
    {
        public EMP_ADDRESS()
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
        
        [Display(Name = "Is Present")]
        public bool IS_PRESENT { get; set; }

        [Display(Name = "Address")]
        [StringLength(500, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ADDRESS_DETAIL { get; set; }
    }
}
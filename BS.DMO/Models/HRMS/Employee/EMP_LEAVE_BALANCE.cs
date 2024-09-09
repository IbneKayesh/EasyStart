namespace BS.DMO.Models.HRMS.Employee
{
    public class EMP_LEAVE_BALANCE : BaseModel
    {
        public EMP_LEAVE_BALANCE()
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
        
        [Display(Name = "Leave Type ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? LEAVE_TYPE_ID { get; set; }

        [Display(Name = "Entitle Qty")]
        [Required(ErrorMessage = "{0} is required")]
        public int ENTITLE_QTY { get; set; }

        [Display(Name = "Used Qty")]
        [Required(ErrorMessage = "{0} is required")]
        public int USED_QTY { get; set; }
    }
}
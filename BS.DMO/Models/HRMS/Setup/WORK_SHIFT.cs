namespace BS.DMO.Models.HRMS.Setup
{
    public class WORK_SHIFT : BaseModel
    {
        public WORK_SHIFT()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Shift Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHIFT_NAME { get; set; }

        [Display(Name = "In Time")]
        public TimeSpan IN_TIME { get; set; }

        [Display(Name = "Out Time")]
        public TimeSpan OUT_TIME { get; set; }

        [Display(Name = "Grace Time")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 480, ErrorMessage = "{0} range is between {2} and {1}")]
        public int GRACE_TIME { get; set; }
    }
}

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

        [Display(Name = "In Time (Start)")]
        public DateTime IN_TIME_START { get; set; }

        [Display(Name = "In Time (End)")]
        public DateTime IN_TIME_END { get; set; }

        [Display(Name = "Out Time (Start)")]
        public DateTime OUT_TIME_START { get; set; }

        [Display(Name = "Out Time (End)")]
        public DateTime OUT_TIME_END { get; set; }

        [Display(Name = "Grace Minute")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 480, ErrorMessage = "{0} range is between {2} and {1}")]
        public int GRACE_MINUTE { get; set; }

        [Display(Name = "Max OT Hours")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 8, ErrorMessage = "{0} range is between {2} and {1}")]
        public int MAX_OT_HOUR { get; set; } = 0;
    }
}

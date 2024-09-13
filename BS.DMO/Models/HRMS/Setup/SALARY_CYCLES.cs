namespace BS.DMO.Models.HRMS.Setup
{
    public class SALARY_CYCLES : BaseModel
    {
        public SALARY_CYCLES()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Cycle Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CYCLE_NAME { get; set; }

        [Display(Name = "Start Day")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 31, ErrorMessage = "{0} range is between {2} and {1}")]
        public int START_DAY { get; set; }

        [Display(Name = "End Day")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(0, 31, ErrorMessage = "{0} range is between {2} and {1}")]
        public int END_DAY { get; set; }
    }
}

namespace BS.DMO.Models.Setup
{
    public class LEAVE_CALENDAR : BaseModel
    {
        //holiday calender
        public LEAVE_CALENDAR()
        {
            ID = Guid.Empty.ToString();
            CALENDAR_DATE = DateTime.Now.Date;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Year Name")]
        [StringLength(11, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? FINANCIAL_YEAR_ID { get; set; }

        [Display(Name = "Leave Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? LEAVE_TYPE_ID { get; set; }


        [Display(Name = "Calendar Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime CALENDAR_DATE { get; set; }


        [Display(Name = "Is Working Day")]
        public bool IS_WORKING_DAY { get; set; }


        [Display(Name = "Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? NOTE_INFO { get; set; }
    }
}

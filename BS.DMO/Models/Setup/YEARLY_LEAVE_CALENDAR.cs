namespace BS.DMO.Models.Setup
{
    public class YEARLY_LEAVE_CALENDAR : BaseModel
    {
        public YEARLY_LEAVE_CALENDAR()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Year Name")]
        [StringLength(11, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? FINANCIAL_YEAR_ID { get; set; }

        [Display(Name = "Holiday Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? HOLIDAY_TYPE_ID { get; set; }

        [Display(Name = "No of Leave")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 50, ErrorMessage = "{0} range is between {2} and {1}")]
        public int NO_OF_LEAVE { get; set; }
    }
}

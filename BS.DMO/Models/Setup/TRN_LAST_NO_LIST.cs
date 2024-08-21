namespace BS.DMO.Models.Setup
{
    public class TRN_LAST_NO_LIST : BaseModel
    {
        public TRN_LAST_NO_LIST()
        {
            DateTime dateTime = DateTime.Now;
            ID = Guid.Empty.ToString();
            LAST_NO = 1;
            YEAR_ID = dateTime.Year;
            MONTH_ID = dateTime.Month;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string? ID { get; set; }


        [Display(Name = "Transaction Id")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TRANSACTION_ID { get; set; }

        [Display(Name = "Last No")]
        [Required(ErrorMessage = "{0} is required")]
        public int LAST_NO { get; set; }


        [Display(Name = "Sub Section Id")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SUB_SECTION_ID { get; set; }

        [Display(Name = "Sub Section Short Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SHORT_NAME { get; set; }

        [Display(Name = "Year No")]
        [Required(ErrorMessage = "{0} is required")]
        public int YEAR_ID { get; set; }

        [Display(Name = "Month No")]
        [Required(ErrorMessage = "{0} is required")]
        public int MONTH_ID { get; set; }
    }
}

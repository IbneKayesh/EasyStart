namespace BS.DMO.Models.Setup
{
    public class FINANCIAL_YEAR : BaseModel
    {
        public FINANCIAL_YEAR()
        {
            //ID = Guid.Empty.ToString();
            DateTime dateTime = DateTime.Now;
            ID = dateTime.ToString("yyyy");
            START_DATE = dateTime;
            END_DATE = dateTime.AddMonths(12);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(4, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 4)]
        public string ID { get; set; }
        //public int YEAR_ID { get; set; }

        [Display(Name = "Year Name")]
        [StringLength(11, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 4)]
        [Required(ErrorMessage = "{0} is required")]
        public string? YEAR_NAME { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime START_DATE { get; set; }


        [Display(Name = "End Date")]
        [Required(ErrorMessage = "{0} is required")]
        public DateTime END_DATE { get; set; }


        [Display(Name = "Locked")]
        public bool IS_LOCKED { get; set; }
    }
}

namespace BS.DMO.Models.Setup
{
    public class TRN_LAST_NO_LIST
    {
        public TRN_LAST_NO_LIST()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string? ID { get; set; }


        [Display(Name = "Transaction Id")]
        [Required(ErrorMessage = "{0} is required")]
        public string? TRANSACTION_ID { get; set; }

        [Display(Name = "Last No")]
        [Required(ErrorMessage = "{0} is required")]
        public int LAST_NO { get; set; }


        [Display(Name = "Section Id")]
        [Required(ErrorMessage = "{0} is required")]
        public string? SECTION_ID { get; set; }

        [Display(Name = "Year No")]
        [Required(ErrorMessage = "{0} is required")]
        public int YEAR_ID { get; set; }

        [Display(Name = "Month No")]
        [Required(ErrorMessage = "{0} is required")]
        public int MONTH_ID { get; set; }
    }
}

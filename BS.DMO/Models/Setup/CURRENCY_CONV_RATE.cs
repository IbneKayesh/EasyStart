namespace BS.DMO.Models.Setup
{
    public class CURRENCY_CONV_RATE : BaseModel
    {
        public CURRENCY_CONV_RATE()
        {
            ID = Guid.Empty.ToString();
            DateTime dateTime = DateTime.Now;
            MONTH_ID = dateTime.ToString("MM");
            YEAR_ID = dateTime.ToString("yyyy");
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Currency Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CURRENCY_ID { get; set; }



        [Display(Name = "Month No")]
        [StringLength(2, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? MONTH_ID { get; set; }

        [Display(Name = "Year No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? YEAR_ID { get; set; }


        [Display(Name = "Conversion Rate")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0.01d, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal? CONVERSION_RATE { get; set; }
    }
}

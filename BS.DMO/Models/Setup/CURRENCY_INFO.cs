namespace BS.DMO.Models.Setup
{
    public class CURRENCY_INFO : BaseModel
    {
        public CURRENCY_INFO()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Country Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? COUNTRY_ID { get; set; }

        [Display(Name = "Currency Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CURRENCY_NAME { get; set; }

        [Display(Name = "Currency Sign")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CURRENCY_SIGN { get; set; }


        [Display(Name = "Currency Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CURRENCY_DESC { get; set; }

        /// <summary>
        /// Base currency should only one in CURRENCY_INFO table
        /// </summary>

        [Display(Name = "Is Base Currency")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_BASE_CURRENCY { get; set; }
    }
}

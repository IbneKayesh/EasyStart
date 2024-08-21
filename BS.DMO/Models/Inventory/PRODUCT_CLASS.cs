namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CLASS : BaseModel
    {
        public PRODUCT_CLASS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Class Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CLASS_NAME { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CLASS_DESC { get; set; }


        //Discount

        [Display(Name = "Allow Discount")]
        public bool IS_DISCOUNT { get; set; }


        [Display(Name = "Total Invoice Value")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum:0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal INVOICE_VALUE { get; set; }


        [Display(Name = "Discount %")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal DISCOUNT_PCT { get; set; }


        [Display(Name = "Discount Value")]
        [Required(ErrorMessage = "{0} is required")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal DISCOUNT_VALUE { get; set; }
    }
}

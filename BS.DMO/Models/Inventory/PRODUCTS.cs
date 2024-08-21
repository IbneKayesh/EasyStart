namespace BS.DMO.Models.Inventory
{
    public class PRODUCTS : BaseModel
    {
        public PRODUCTS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Business Line")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BUSINESS_LINE_ID { get; set; }

        [Display(Name = "Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_TYPE_ID { get; set; }

        [Display(Name = "Class")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_CLASS_ID { get; set; }

        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_CATEGORY_ID { get; set; }

        [Display(Name = "Brand")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_BRAND_ID { get; set; }

        [Display(Name = "UOM")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_CHILD_ID { get; set; }


        [Display(Name = "Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_CODE { get; set; }

        [Display(Name = "Barcode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BAR_CODE { get; set; }

        [Display(Name = "Product Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_NAME { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_DESC { get; set; }


        [Display(Name = "Image")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_IMG { get; set; }


        [Display(Name = "Last Purchase Rate")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LAST_PURCHASE_RATE { get; set; }

        [Display(Name = "Last Sales Rate")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LAST_SALES_RATE { get; set; }



        [Display(Name = "Has Warranty")]
        [Required(ErrorMessage = "{0} is required")]
        public bool HAS_WARRANTY { get; set; } = false;

        [Display(Name = "Has Expiry")]
        [Required(ErrorMessage = "{0} is required")]
        public bool HAS_EXPIRY { get; set; } = false;

        [Display(Name = "Is Main Product")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_MAIN_PRODUCT { get; set; } = true;

        [Display(Name = "VAT %")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal VAT_PCT { get; set; }

        [Display(Name = "Base Price")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal BASE_PRICE { get; set; }


        [Display(Name = "Weight Per Unit")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WEIGHT_PER_UNIT { get; set; }

        [Display(Name = "Weight Formula")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WEIGHT_FORMULA { get; set; }

        [Display(Name = "Net Weight")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? NET_WEIGHT { get; set; }


        [Display(Name = "Price Formula")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRICE_FORMULA { get; set; }

        [Display(Name = "Net Price")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal NET_PRICE { get; set; }

    }
}

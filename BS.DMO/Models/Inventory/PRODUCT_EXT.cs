namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_EXT : BaseModel
    {
        //stock will maintain with child as a ref id
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? ID { get; set; }

        [Display(Name = "Image")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_IMG { get; set; }

        [Display(Name = "Special Instruction")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SPECIAL_INSTRUCTION { get; set; }

        [Display(Name = "Weight Formula")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WEIGHT_FORMULA { get; set; }

        [Display(Name = "Net Weight (KG)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal NET_WEIGHT { get; set; }

        //With Product Child Items Value [text], (child items * child items qty * child items value)
        [Display(Name = "Price Formula")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRICE_FORMULA { get; set; }

        [Display(Name = "Net Price")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal NET_PRICE { get; set; }

        [Display(Name = "Height (Inch)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal HEIGHT_INCH { get; set; }

        [Display(Name = "Height (CM)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal HEIGHT_CM { get; set; }

        [Display(Name = "Height (Inch)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal WIDTH_INCH { get; set; }

        [Display(Name = "Width (CM)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal WIDTH_CM { get; set; }

        [Display(Name = "Length (Inch)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LENGTH_INCH { get; set; }

        [Display(Name = "Length (CM)")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal LENGTH_CM { get; set; }

        [Display(Name = "Min Stock Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal MIN_STOCK_QTY { get; set; }

        [Display(Name = "Max Stock Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal MAX_STOCK_QTY { get; set; }

        [Display(Name = "Purchase Target Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PURCHASE_TARGET_QTY { get; set; }

        [Display(Name = "Sales Target Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal SALES_TARGET_QTY { get; set; }

        [Display(Name = "Allow Qty Split")]
        public bool ALLOW_QTY_SPLIT { get; set; }

        [Display(Name = "Product Note")]
        [StringLength(250, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_NOTE { get; set; }

        [Display(Name = "Color")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? COLOR_ID { get; set; }

        [Display(Name = "Size")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SIZE_ID { get; set; }
        
        [Display(Name = "Brand")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_BRAND_ID { get; set; }

        [Display(Name = "Origin")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? COUNTRY_ID { get; set; }

        [Display(Name = "Package Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PACK_ID { get; set; }

        [Display(Name = "Grade")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? GRADE_ID { get; set; }

        [Display(Name = "Claim Type")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CLAIM_ID { get; set; }

        [Display(Name = "Group")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_GROUP_ID { get; set; }

        [Display(Name = "Source")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_SOURCE_ID { get; set; }

        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_STATUS_ID { get; set; }        
    }
}

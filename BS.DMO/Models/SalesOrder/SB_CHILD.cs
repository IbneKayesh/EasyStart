namespace BS.DMO.Models.SalesOrder
{
    public class SB_CHILD : BaseModel
    {
        public SB_CHILD()
        {
            ID = Guid.Empty.ToString();
            DELIVERY_DATE = DateTime.Now.AddDays(3);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MASTER_ID { get; set; }



        [Display(Name = "Line No")]
        public int LINE_NO { get; set; }

        [Display(Name = "Ref Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? REF_TRN_LINE_ID { get; set; } = "";



        [Display(Name = "Product Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_ID { get; set; }

        [Display(Name = "Main Product Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? MAIN_PRODUCT_ID { get; set; }

        [Display(Name = "Pack Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PACK_ID { get; set; }

        [Display(Name = "Product Description")]
        [StringLength(300, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_DESC { get; set; }



        [Display(Name = "Unit Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_ID { get; set; }

        [Display(Name = "Rate")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_RATE { get; set; }

        [Display(Name = "Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_QTY { get; set; }


        [Display(Name = "VAT%")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal VAT_PCT { get; set; }

        [Display(Name = "Discount%")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal DISCOUNT_PCT { get; set; }

        [Display(Name = "Discount Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal DISCOUNT_AMOUNT { get; set; }


        [Display(Name = "Amount")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_AMOUNT { get; set; }

        
        [Display(Name = "Weight")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_WEIGHT { get; set; }


        [Display(Name = "Ref/SKU")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? REF_SKU { get; set; }

        [Display(Name = "Line Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? LINE_NOTE { get; set; }


        [Display(Name = "Packing Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PACKING_NOTE { get; set; }

        [Display(Name = "Delivery Address")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CONTACT_ADDRESS_ID { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime DELIVERY_DATE { get; set; }
    }
}

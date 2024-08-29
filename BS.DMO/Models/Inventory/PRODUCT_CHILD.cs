namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CHILD : BaseModel
    {
        [Display(Name = "Product ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_ID { get; set; }

        [Display(Name = "Product Child ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_CHILD_ID { get; set; }

        [Display(Name = "Product Child Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_CHILD_QTY { get; set; }

        [Display(Name = "Wastage Child %")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_CHILD_WASTAGE_PCT { get; set; }

        [Display(Name = "Wastage Child Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_CHILD_WASTAGE_QTY { get; set; }

        [Display(Name = "Product Child Net Qty")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_CHILD_NET_QTY { get; set; }

        [Display(Name = "Product Child Value")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal PRODUCT_CHILD_VALUE { get; set; }

        [Display(Name = "Raw Material")]
        public bool IS_RM { get; set; }

        [Display(Name = "Finished Goods")]
        public bool IS_FG { get; set; }
    }
}

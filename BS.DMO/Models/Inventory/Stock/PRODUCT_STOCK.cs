namespace BS.DMO.Models.Inventory.Stock
{
    public class PRODUCT_STOCK : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "Detail Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ID { get; set; }

        [Display(Name = "Master Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MASTER_ID { get; set; }

        [Display(Name = "Sub Section Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SUB_SECTION_ID { get; set; }

        [Display(Name = "Trn Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TRN_ID { get; set; } //PO, Adjust

        [Display(Name = "Product Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_ID { get; set; }

        [Display(Name = "Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? PRODUCT_CODE { get; set; }

        [Display(Name = "Barcode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BAR_CODE { get; set; }

        [Display(Name = "Model No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MODEL_NO { get; set; }

        [Display(Name = "Serial No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SERIAL_NO { get; set; }

        [Display(Name = "Batch No")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? BATCH_NO { get; set; }

        [Display(Name = "MFG Date")]
        public DateTime? MFG_DATE { get; set; }

        [Display(Name = "Last Audit Date")]
        public DateTime? LAST_AUDIT_DATE { get; set; }

        [Display(Name = "Warranty In Month")]
        public int? WARRANTY_IN_MONTH { get; set; } = 0;

        [Display(Name = "Date of Purchase")]
        public DateTime? DATE_OF_PURCHASE { get; set; }

        [Display(Name = "Warranty End Date")]
        public DateTime? WARRANTY_END_DATE { get; set; }

        [Display(Name = "License End Date")]
        public DateTime? LICENSE_END_DATE { get; set; }

        [Display(Name = "Expairy Date")]
        public DateTime? EXPAIRY_DATE { get; set; }

        [Display(Name = "Max Life Time In Month")]
        public int MAX_LIFE_TIME { get; set; }

        [Display(Name = "Depreciation Value Per Month")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DEPRECIATION_RATE { get; set; }

        [Display(Name = "Current Value")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CURRENT_VALUE { get; set; }

        [Display(Name = "Next Maintenance Date")]
        public DateTime? NEXT_MAINTENANCE_DATE { get; set; }

        [Display(Name = "Is Available")]
        public bool IS_AVAILABLE { get; set; }

        [Display(Name = "Product Qty")] //Good Qty
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PRODUCT_QTY { get; set; } //+

        [Display(Name = "Product Rate")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PRODUCT_RATE { get; set; }

        [Display(Name = "Total Value")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TOTAL_VALUE { get; set; }

        [Display(Name = "Return Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RETURN_QTY { get; set; } //-

        [Display(Name = "Consume Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal CONSUMED_QTY { get; set; } //-

        [Display(Name = "Damaged Qty")] //Bad Qty
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DAMAGED_QTY { get; set; } //-

        [Display(Name = "Damaged Rate")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal DAMAGED_RATE { get; set; }

        [Display(Name = "Issue Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ISSUE_QTY { get; set; } //-

        [Display(Name = "Receive Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RECEIVE_QTY { get; set; } //+

        [Display(Name = "Adjust In Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ADJ_IN_QTY { get; set; } //+

        [Display(Name = "Adjust Out Qty")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ADJ_OUT_QTY { get; set; } //-

        [Display(Name = "In Process")]
        [Column(TypeName = "decimal(18, 4)")] //-
        public decimal INP_QTY { get; set; }

        [Display(Name = "OHQ")]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OHQ_QTY { get; set; } //=
    }
}

namespace BS.DMO.Models.Inventory
{
    public class ITEM_MASTER : BaseModel
    {
        public ITEM_MASTER()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Sub Group Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_SUB_GROUP_ID { get; set; }

        [Display(Name = "Class")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_CLASS_ID { get; set; }

        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_CATEGORY_ID { get; set; }

        [Display(Name = "Type Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_TYPE_ID { get; set; }

        [Display(Name = "Status Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_STATUS_ID { get; set; }

        [Display(Name = "UOM")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_CHILD_ID { get; set; }

        [Display(Name = "Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MASTER_ITEM_CODE { get; set; }

        [Display(Name = "Barcode")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? MASTER_BAR_CODE { get; set; }

        [Display(Name = "Item Name")]
        [StringLength(200, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_NAME { get; set; }

        [Display(Name = "Warranty (Days)")]
        [Required(ErrorMessage = "{0} is required")]
        public int WARRANTY_DAYS { get; set; } = 0;

        [Display(Name = "Expiry (Days)")]
        [Required(ErrorMessage = "{0} is required")]
        public int EXPIRY_DAYS { get; set; } = 0;

        [Display(Name = "Is Main Item")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_MAIN_ITEM { get; set; } = true;

        [Display(Name = "VAT %")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal VAT_PCT { get; set; }

        [Display(Name = "Base Price")]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal BASE_PRICE { get; set; }

        [Display(Name = "Image")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_IMG { get; set; }

        [Display(Name = "Special Instruction")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? SPECIAL_INSTRUCTION { get; set; }
    }
}

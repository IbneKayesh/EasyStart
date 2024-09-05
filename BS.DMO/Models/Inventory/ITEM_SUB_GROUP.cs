namespace BS.DMO.Models.Inventory
{
    public class ITEM_SUB_GROUP : BaseModel
    {
        public ITEM_SUB_GROUP()
        {
            ID = Guid.Empty.ToString();
            HS_CODE = "0";
            LEAD_DAYS = 1;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Group Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_GROUP_ID { get; set; }


        [Display(Name = "Sub Group Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_SUB_GROUP_NAME { get; set; }


        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_SUB_GROUP_DESC { get; set; }

        [Display(Name = "FG / RM")]
        public bool IS_FG_RM { get; set; }

        [Display(Name = "Generate Stock")]
        public bool IS_STOCK { get; set; }

        [Display(Name = "HS Code")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? HS_CODE { get; set; }

        [Display(Name = "Code Prefix")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CODE_PREFIX { get; set; }

        [Display(Name = "Lead Days")]
        [Range(1, 1865, ErrorMessage = "{0} length is {2} between {1}")]
        public int LEAD_DAYS { get; set; }

        [Display(Name = "Image")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_SUB_GROUP_IMG { get; set; }
    }
}

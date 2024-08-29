namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_TYPE : BaseModel
    {
        public PRODUCT_TYPE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TYPE_NAME { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TYPE_DESC { get; set; }

        [Display(Name = "Generate Stock")]
        public bool IS_STOCK { get; set; }
    }
}

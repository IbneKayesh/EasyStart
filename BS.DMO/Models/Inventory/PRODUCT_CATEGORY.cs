namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CATEGORY : BaseModel
    {
        public PRODUCT_CATEGORY()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Category Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CATEGORY_NAME { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CATEGORY_DESC { get; set; }

        [Display(Name = "Image")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CATEGORY_IMG { get; set; }
    }
}

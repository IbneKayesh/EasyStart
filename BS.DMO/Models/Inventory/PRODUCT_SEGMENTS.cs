namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_SEGMENTS : BaseModel
    {
        public PRODUCT_SEGMENTS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Product Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PRODUCT_ID { get; set; }

        [Display(Name = "Segment Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SEGMENT_NAME_TYPE_ID { get; set; }


        [Display(Name = "Segment Value")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SEGMENT_VALUE { get; set; }
    }
}

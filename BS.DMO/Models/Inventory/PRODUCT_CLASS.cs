namespace BS.DMO.Models.Inventory
{
    public class PRODUCT_CLASS : BaseModel
    {
        public PRODUCT_CLASS()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Class Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? CLASS_NAME { get; set; }

        [Display(Name = "Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? CLASS_DESC { get; set; }




        [Display(Name = "Is Master Product")]
        public bool IS_MASTER_PRODUCT { get; set; }

        [Display(Name = "Allow Purchase")]
        public bool IS_PURCHASE { get; set; }

        [Display(Name = "Allow Sales")]
        public bool IS_SALES { get; set; }

        [Display(Name = "Allow Discount")]
        public bool IS_DISCOUNT { get; set; }
    }
}

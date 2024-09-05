namespace BS.DMO.Models.Inventory
{
    public class ITEM_CATEGORY : BaseModel
    {
        public ITEM_CATEGORY()
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


        //For Central Management
        [Display(Name = "Stop Purchase")]
        public bool STOP_PURCHASE { get; set; }

        [Display(Name = "Stop Sales")]
        public bool STOP_SALES { get; set; }

        [Display(Name = "Stop Discount")]
        public bool STOP_DISCOUNT { get; set; }

    }
}

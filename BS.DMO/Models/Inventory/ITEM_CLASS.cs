namespace BS.DMO.Models.Inventory
{
    public class ITEM_CLASS : BaseModel
    {
        public ITEM_CLASS()
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




        [Display(Name = "Is Main Product")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_MAIN_PRODUCT { get; set; } = true;

        [Display(Name = "Allow Qty Split")]
        public bool ALLOW_QTY_SPLIT { get; set; } = false;
    }
}

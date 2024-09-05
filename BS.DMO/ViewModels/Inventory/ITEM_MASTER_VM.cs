namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_MASTER_VM : ITEM_MASTER
    {
        //[NotMapped]
        [Display(Name = "Business Line Name")]
        public string? BUSINESS_LINE_NAME { get; set; }

        [Display(Name = "Type Name")]
        public string? TYPE_NAME { get; set; }

        [Display(Name = "Class Name")]
        public string? CLASS_NAME { get; set; }

        [Display(Name = "Category Name")]
        public string? CATEGORY_NAME { get; set; }

        [Display(Name = "UOM")]
        public string? UNIT_NAME { get; set; }
    }
}

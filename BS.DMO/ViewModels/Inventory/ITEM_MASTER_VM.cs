namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_MASTER_VM : ITEM_MASTER
    {
        //[NotMapped]
        [Display(Name = "Sub Group Name")]
        public string? ITEM_SUB_GROUP_NAME { get; set; }

        [Display(Name = "Class Name")]
        public string? CLASS_NAME { get; set; }

        [Display(Name = "Category Name")]
        public string? CATEGORY_NAME { get; set; }

        [Display(Name = "Type Name")]
        public string? TYPE_NAME { get; set; }

        [Display(Name = "Status Name")]
        public string? STATUS_NAME { get; set; }

        [Display(Name = "UOM")]
        public string? UNIT_NAME { get; set; }
    }
}

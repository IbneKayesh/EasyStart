namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_SUB_GROUP_VM : ITEM_SUB_GROUP
    {
        //[NotMapped]

        [Display(Name = "Item Group (Business Line)")]
        public string? ITEM_GROUP_NAME { get; set; }

        [Display(Name = "Setup")]
        public int? ITEM_SETUP_COUNT { get; set; } = 0;

    }
}

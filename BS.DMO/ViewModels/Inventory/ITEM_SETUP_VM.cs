namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_SETUP_VM : ITEM_SETUP
    {
        //[NotMapped]
        [Display(Name = "Attribute Name")]
        public string? ITEM_ATTRIBUTE_NAME { get; set; }
    }
}

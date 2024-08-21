using BS.DMO.Models.Inventory;

namespace BS.DMO.ViewModels.Inventory
{
    public class UNIT_CHILD_VM : UNIT_CHILD
    {
        //[NotMapped]
        [Display(Name = "UOM Master Group Name")]
        public string? UNIT_MASTER_NAME { get; set; }
    }
}

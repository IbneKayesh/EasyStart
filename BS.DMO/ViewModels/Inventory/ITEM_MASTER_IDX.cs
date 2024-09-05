namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_MASTER_IDX
    {
        public string? type_name { get; set; }
        public string? class_name { get; set; }
        public string? category_name { get; set; }
        public string? product_name { get; set; }
        public List<ITEM_MASTER_VM> ITEM_MASTER_VM {  get; set; }
    }
}

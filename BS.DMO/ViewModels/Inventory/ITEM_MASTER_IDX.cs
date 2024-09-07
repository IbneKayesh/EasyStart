namespace BS.DMO.ViewModels.Inventory
{
    public class ITEM_MASTER_IDX
    {
        public string? item_name { get; set; }
        public string? item_sub_group_name { get; set; }
        public string? class_name { get; set; }
        public string? category_name { get; set; }
        public string? item_code { get; set; }
        public List<ITEM_MASTER_VM>? ITEM_MASTER_VM {  get; set; }
    }
}

namespace BS.DMO.ViewModels.Inventory
{
    public class PRODUCTS_IDX
    {
        public string? type_name { get; set; }
        public string? class_name { get; set; }
        public string? category_name { get; set; }
        public string? product_name { get; set; }
        public List<PRODUCTS_VM> PRODUCTS_VM {  get; set; }
    }
}

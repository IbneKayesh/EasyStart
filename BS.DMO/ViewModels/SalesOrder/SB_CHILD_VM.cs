namespace BS.DMO.ViewModels.SalesOrder
{
    public class SB_CHILD_VM : SB_CHILD
    {
        //[NotMapped]
        [Display(Name = "Product Name")]
        public string? PRODUCT_NAME { get; set; }

        [Display(Name = "UOM Name")]
        public string? UNIT_NAME { get; set; }

        [Display(Name = "Delivery Address")]
        public string? DELIVERY_ADDRESS { get; set; }
    }
}

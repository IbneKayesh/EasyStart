namespace BS.DMO.Models.Inventory
{
    public class ITEM_ATTRIBUTE_VALUE : BaseModel
    {
        public ITEM_ATTRIBUTE_VALUE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string ID { get; set; }

        [Display(Name = "Item Attribute Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_ATTRIBUTE_ID { get; set; }

        [Display(Name = "Attribute Value")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ATTRIBUTE_VALUE { get; set; }
    }
}

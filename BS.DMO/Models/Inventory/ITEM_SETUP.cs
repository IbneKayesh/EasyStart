namespace BS.DMO.Models.Inventory
{
    public class ITEM_SETUP : BaseModel
    {
        public ITEM_SETUP()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Sub Group Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_SUB_GROUP_ID { get; set; }

        [Display(Name = "Item Attribute Value Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_ATTRIBUTE_VALUE_ID { get; set; }

        [Display(Name = "Item Attribute Id")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_ATTRIBUTE_ID { get; set; }

        [Display(Name = "Default Value")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? DEFAULT_VALUE { get; set; }

        [Display(Name = "Add to Name")]
        public bool ADD_TO_NAME { get; set; } = false;
    }
}

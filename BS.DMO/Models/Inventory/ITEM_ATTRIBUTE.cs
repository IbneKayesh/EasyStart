namespace BS.DMO.Models.Inventory
{
    public class ITEM_ATTRIBUTE : BaseModel
    {
        public ITEM_ATTRIBUTE()
        {
            ID = Guid.Empty.ToString();
            ADD_TO_NAME = true;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string ID { get; set; }

        [Display(Name = "Attribute Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_ATTRIBUTE_NAME { get; set; }

        [Display(Name = "Attribute Short Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_ATTRIBUTE_SHORT_NAME { get; set; }

        [Display(Name = "Add to Name")]
        public bool ADD_TO_NAME { get; set; }


        //[Display(Name = "Data Type Name")]
        //[StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        //[Required(ErrorMessage = "{0} is required")]
        //public string? SEGMENT_DATA_TYPE { get; set; }

        [NotMapped]
        public List<ITEM_ATTRIBUTE_VALUE>? ITEM_ATTRIBUTE_VALUE { get; set; }
    }
}

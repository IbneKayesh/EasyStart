namespace BS.DMO.Models.Inventory
{
    public class ITEM_GROUP_TYPE : BaseModel
    {
        public ITEM_GROUP_TYPE()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Group Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? ITEM_GROUP_TYPE_NAME { get; set; }

        [Display(Name = "Group Type Description")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ITEM_GROUP_TYPE_DESC { get; set; }
    }
}

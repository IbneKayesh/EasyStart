namespace BS.DMO.Models.Inventory
{
    public class UNIT_MASTER : BaseModel
    {
        public UNIT_MASTER()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "UOM Master Group Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_MASTER_NAME { get; set; }

        [Display(Name = "UOM Master Group Short Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? UNIT_MASTER_SHORT_NAME { get; set; }
    }
}
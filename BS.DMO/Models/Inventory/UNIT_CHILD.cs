namespace BS.DMO.Models.Inventory
{
    public class UNIT_CHILD : BaseModel
    {
        public UNIT_CHILD()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "UOM Master Group Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_MASTER_ID { get; set; }



        [Display(Name = "UOM Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? UNIT_NAME { get; set; }


        [Display(Name = "UOM Short Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? SHORT_NAME { get; set; }


        [Display(Name = "Relative Factor")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(minimum: 0, int.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public int RELATIVE_FACTOR { get; set; }
    }
}

namespace BS.DMO.Models.SFM.Setup
{
    public class PROCESS_MASTER : BaseModel
    {
        public PROCESS_MASTER()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Business Line")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BUSINESS_LINE_ID { get; set; }

        //Process Name ex- Boil and Fry
        [Display(Name = "Process Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PROCESS_NAME { get; set; }

        [Display(Name = "Sequence No")]
        [Range(1, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} is required")]
        public int SEQUENCE_NO { get; set; }

        [Display(Name = "Process in Sec (Per Unit, Sum of Child)")]
        [Range(1, 1296000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} is required")]
        public int PROCESS_IN_SEC { get; set; }
    }
}
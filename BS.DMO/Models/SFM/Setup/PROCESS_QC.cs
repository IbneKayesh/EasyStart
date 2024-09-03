namespace BS.DMO.Models.SFS.Setup
{
    public class PROCESS_QC : BaseModel
    {
        public PROCESS_QC()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Process Master ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? PROCESS_MASTER_ID { get; set; }

        [Display(Name = "QC Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 2)]
        [Required(ErrorMessage = "{0} is required")]
        public string? QC_NAME { get; set; }

        [Display(Name = "Sequence No")]
        [Range(1, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} is required")]
        public int SEQUENCE_NO { get; set; }

        [Display(Name = "Process in Sec (Per Unit)")]
        [Range(1, 1296000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [Required(ErrorMessage = "{0} is required")]
        public int PROCESS_IN_SEC { get; set; }

    }
}

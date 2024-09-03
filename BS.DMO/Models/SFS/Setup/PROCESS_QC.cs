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
        [StringLength(maximum: 9999, ErrorMessage = "{0} range is {2} between {1}", MinimumLength = 1 )]
        [Required(ErrorMessage = "{0} is required")]
        public int SEQUENCE_NO { get; set; }

        [Display(Name = "Process in Sec (Per Unit)")]
        [StringLength(maximum: 1296000, ErrorMessage = "{0} range is {2} between {1}", MinimumLength = 1 )]
        [Required(ErrorMessage = "{0} is required")]
        public int PROCESS_IN_SEC { get; set; }
        
    }
}

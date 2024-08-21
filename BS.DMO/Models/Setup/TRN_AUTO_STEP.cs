namespace BS.DMO.Models.Setup
{
    public class TRN_AUTO_STEP : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Transaction ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? TRN_ID { get; set; }


        [Display(Name = "Auto Post")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_POSTED { get; set; } = false;


        [Display(Name = "Auto Approve")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_APPROVE { get; set; } = false;

    }
}

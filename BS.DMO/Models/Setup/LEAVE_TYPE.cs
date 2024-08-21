namespace BS.DMO.Models.Setup
{
    public class LEAVE_TYPE : BaseModel
    {
        public LEAVE_TYPE()
        {
            ID = Guid.Empty.ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }


        [Display(Name = "Leave Type Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? LEAVE_TYPE_NAME { get; set; }


        [Display(Name = "Is Working Day")]
        [Required(ErrorMessage = "{0} is required")]
        public bool IS_WORKING_DAY { get; set; } = false;
    }
}

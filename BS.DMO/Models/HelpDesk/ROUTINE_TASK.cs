namespace BS.DMO.Models.HelpDesk
{
    public class ROUTINE_TASK : BaseModel
    {
        public ROUTINE_TASK()
        {
            ID = Guid.Empty.ToString();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? USER_ID { get; set; }

        [Display(Name = "Routine Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_NAMES_ID { get; set; }

        [Display(Name = "Routine Date")]
        public DateTime ROUTINE_DATE { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_NOTE{ get; set; }

        [Display(Name = "Done")]
        public bool IS_DONE { get; set; }
    }
}

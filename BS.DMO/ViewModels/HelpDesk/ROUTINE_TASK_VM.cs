namespace BS.DMO.ViewModels.HelpDesk
{
    public class ROUTINE_TASK_VM
    {
        public ROUTINE_TASK_VM()
        {
            ID = Guid.Empty.ToString();
        }
        [Display(Name = "Routine Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_NAME { get; set; }

        [Display(Name = "Frequency")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_FREQUENCY { get; set; }

        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string? ID { get; set; }

        [Display(Name = "Routine Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_NAMES_ID { get; set; }

        [Display(Name = "Routine Date")]
        public DateTime? ROUTINE_DATE { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? ROUTINE_NOTE { get; set; }

        [Display(Name = "Done")]
        public string IS_DONE { get; set; }
    }
}

namespace BS.DMO.ViewModels.HelpDesk
{
    public class WORK_TASK_VM : WORK_TASK
    {
        //[NotMapped]

        //[Display(Name = "Parent Title")]
        //public string? PARENT_TITLE { get; set; }

        [NotMapped]
        [Display(Name = "Wait Duration")]
        public string? WAIT_DURATION { get; set; }
        
        [NotMapped]
        [Display(Name = "Total Duration")]
        public string? TOTAL_DURATION { get; set; }

        [Display(Name = "Status")]
        public string? STATUS_NAME { get; set; }

        [Display(Name = "Status Color")]
        public string? BS_COLOR { get; set; }
    }
}

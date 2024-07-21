namespace BS.DMO.Models.HelpDesk
{

    public class WORK_TASK
    {
        public string ID { get; set; }
        public string? PARENT_ID { get; set; }
        public string? BG_ID { get; set; }
        public string? STATUS_ID { get; set; }
        public string? PRIORITY_ID { get; set; }


        public string? TASK_TITLE { get; set; }
        public string? TASK_DESC { get; set; }


        public int PROGRESS_PCT { get; set; }
        public string? TAG_LIST { get; set; }
        public string? TASK_REMARKS { get; set; }

        public string? REQUEST_USER { get; set; }
        public DateTime REQUEST_DATE { get; set; }

        public string? L1_USER { get; set; }
        public DateTime? L1_DATE { get; set; }
        public string? L2_USER { get; set; }
        public DateTime? L2_DATE { get; set; }

        public string? END_USER { get; set; }
        public DateTime? END_DATE { get; set; }
    }
}

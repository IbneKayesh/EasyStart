namespace BS.DMO.Models.HelpDesk
{
    public class WORK_TASK : BaseModel
    {
        public WORK_TASK()
        {
            ID = Guid.Empty.ToString();
            PARENT_ID = Guid.Empty.ToString();
            BG_ID = Guid.Empty.ToString();
            STATUS_ID = Guid.Empty.ToString();
            PRIORITY_ID = Guid.Empty.ToString();
            TASK_VALUE = 0;
            TOTAL_WORK_HOURS = 0.1m;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "{0} is required")]
        public string? WT_TYPE { get; set; }

        [Display(Name = "Parent")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        //[Required(ErrorMessage = "{0} is required")]
        public string? PARENT_ID { get; set; }

        [Display(Name = "Board Group")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        //[Required(ErrorMessage = "{0} is required")]
        public string? BG_ID { get; set; }

        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        //[Required(ErrorMessage = "{0} is required")]
        public string? STATUS_ID { get; set; }

        [Display(Name = "Priority")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        //[Required(ErrorMessage = "{0} is required")]
        public string? PRIORITY_ID { get; set; }

        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        [Required(ErrorMessage = "{0} is required")]
        public string? TASK_TITLE { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TASK_DESC { get; set; }


        [Display(Name = "Task File")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TASK_FILE { get; set; }

        [Display(Name = "Progress %")]
        public int PROGRESS_PCT { get; set; }

        [Display(Name = "Task Value")]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(minimum: 0, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal TASK_VALUE { get; set; }

        [Display(Name = "Tag (separated by comma)")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? TAG_LIST { get; set; }



        [Display(Name = "Request User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? REQUEST_USER { get; set; }

        [Display(Name = "Request Date")]
        public DateTime REQUEST_DATE { get; set; }

        [Display(Name = "L1 User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? L1_USER { get; set; }

        [Display(Name = "L1 Date")]
        public DateTime? L1_DATE { get; set; }

        [Display(Name = "L1 Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? L1_NOTE { get; set; }

        [Display(Name = "L2 User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? L2_USER { get; set; }

        [Display(Name = "L2 Date")]
        public DateTime? L2_DATE { get; set; }

        [Display(Name = "L2 Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? L2_NOTE { get; set; }

        [Display(Name = "End User")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? END_USER { get; set; }

        [Display(Name = "End Date")]
        public DateTime? END_DATE { get; set; }

        [Display(Name = "End Note")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? END_NOTE { get; set; }


        [Display(Name = "Work Remarks")]
        [StringLength(100, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WORK_REMARKS { get; set; }

        [Display(Name = "Task File")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        public string? WORK_FILE { get; set; }


        [Display(Name = "Work Start Date")]
        public DateTime? WORK_START_DATE { get; set; }

        [Display(Name = "Work End Date")]
        public DateTime? WORK_END_DATE { get; set; }

        [Display(Name = "Total Work Hours")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(minimum: 0.01d, double.MaxValue, ErrorMessage = "{0} length is {2} between {1}")]
        public decimal TOTAL_WORK_HOURS { get; set; }


        [NotMapped]
        [Display(Name = "Board")]
        public string? BOARD_ID { get; set; }
    }
}

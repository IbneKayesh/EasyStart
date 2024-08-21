namespace BS.DMO.Models.HelpDesk
{
    public class BOARD_GROUP : BaseModel
    {
        public BOARD_GROUP()
        {
            ID = Guid.Empty.ToString();
            WORK_TASK_VM = new List<WORK_TASK_VM>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 1)]
        public string ID { get; set; }

        [Display(Name = "Board")]
        [Required(ErrorMessage = "{0} is required")]
        public string? BOARD_ID { get; set; }

        [Display(Name = "Group Name")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} is required")]
        public string? GROUP_NAME { get; set; }

        [Display(Name = "Color")]
        [StringLength(50, ErrorMessage = "{0} length is {2} between {1}", MinimumLength = 0)]
        [Required(ErrorMessage = "{0} is required")]
        public string? BS_COLOR { get; set; }

        [Display(Name = "Order By")]
        [Required(ErrorMessage = "{0} is required")]
        public int? ORDER_BY { get; set; } = 1;

        [Display(Name = "Limit Rows")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(5, 1000, ErrorMessage = "{0} length is between {2} and {1}")]
        public int LIMIT_ROWS { get; set; } = 10;




        //[Display(Name = "Board Name")]
        //[NotMapped]
        //public string? BOARD_NAME { get; set; }

        [Display(Name = "Work Task")]
        [NotMapped]
        public List<WORK_TASK_VM>? WORK_TASK_VM { get; set; }

    }
}

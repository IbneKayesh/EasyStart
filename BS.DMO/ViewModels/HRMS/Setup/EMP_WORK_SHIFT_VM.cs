using BS.DMO.Models.HRMS.Employee;

namespace BS.DMO.ViewModels.HRMS.Setup
{
    public class EMP_WORK_SHIFT_VM : EMP_WORK_SHIFT
    {
        //[NotMapped]
        [Display(Name = "Shift Name")]
        public string? SHIFT_NAME { get; set; }

        [Display(Name = "In Time (Start)")]
        public DateTime IN_TIME_START { get; set; }

        [Display(Name = "In Time (End)")]
        public DateTime IN_TIME_END { get; set; }

        [Display(Name = "Out Time (Start)")]
        public DateTime OUT_TIME_START { get; set; }

        [Display(Name = "Out Time (End)")]
        public DateTime OUT_TIME_END { get; set; }

        [Display(Name = "Grace Minute")]
        public int GRACE_MINUTE { get; set; }

        [Display(Name = "Max OT Hours")]
        public int MAX_OT_HOUR { get; set; } = 0;
    }
}

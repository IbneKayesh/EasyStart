namespace BS.DMO.ViewModels.HRMS.Setup
{
    public class EMP_WORK_SHIFT_VM_IDX
    {

        [Display(Name = "Employee ID")]
        public string? EMP_ID { get; set; }
        public List<EMP_WORK_SHIFT_VM>? EMP_WORK_SHIFT_VM { get; set; }
    }
}

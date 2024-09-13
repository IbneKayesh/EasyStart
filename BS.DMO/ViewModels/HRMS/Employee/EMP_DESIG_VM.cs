namespace BS.DMO.ViewModels.HRMS.Employee
{
    public class EMP_DESIG_VM : EMP_DESIG
    {

        //[Not Mapped]

        [Display(Name = "Sub Section Name")]
        public string? SUB_SECTION_NAME { get; set; }

        [Display(Name = "Designation Name")]
        public string? DESIGNATION_NAME { get; set; }
    }
}

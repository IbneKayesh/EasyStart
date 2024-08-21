namespace BS.DMO.ViewModels.HRMS.Setup
{
    public class DESIGNATION_VM : DESIGNATION
    {

        //[NotMapped]
        [Display(Name = "Parent Name")]
        public string? PARENT_NAME { get; set; }
    }
}

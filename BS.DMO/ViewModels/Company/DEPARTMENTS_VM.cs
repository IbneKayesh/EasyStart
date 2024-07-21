namespace BS.DMO.ViewModels.Company
{
    public class DEPARTMENTS_VM : DEPARTMENTS
    {
        //[NotMapped]

        [Display(Name = "Branch Name")]
        public string? BRANCH_NAME { get; set; }
    }
}
